using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace XmlConvertForIstok.Convert
{
    public enum TepOrManual { Tep,Manual}
    public class ExcelTexRead
    {
        private StationBilder _stationBilder;
        private XLWorkbook _wBook ;

        private const string TableType = "Folder";
        private const string TablePropertyName = "sortindex";
        private const string TablePropertyType = "string";

        private const string TampleName = "тэц";
        private const string TampleType1 = "ManualGate";
        private const string TampleType2 = "TEPTemplate";
        private const string TamplePropertyType = "string";
        private const string TamplePropertyName = "interval";
        private const string TamplePropertyValue = "[UTC+04]=1d";
        private const string TampleProperty2Name = "sortindex";
        private const string TampleProperty2Type = "string";

	    private const string SignalType1 = "ManualParameter";
	    private const string SignalType2 = "TEP";
	    private const string SignalProp1Name = "formula_cod";
	    private const string SignalProp11Name = "formula_text";
	    private const string SignalProp2Name = "index";
	    private const string SignalProp3Name = "sortindex";
	    private const string SignalPropType = "string";

        public ExcelTexRead(StationBilder stationBilder, string filename)
        {
            _stationBilder = stationBilder;
            _wBook = new XLWorkbook(filename);
        }
        public void CreateStation(string stationname)
        {
            _stationBilder.NameStation(stationname).TypeStation("Station");
			_stationBilder.AddPropertyStation ().Name (TablePropertyName).Type (TablePropertyType).Value ("1");
        }
        public TableBilder ReadTable(string sheetname)
        {
            var sheet = _wBook.Worksheet(sheetname);
            var tablenName = sheet.FirstCellUsed().GetString().Trim();

            var tablebilder = _stationBilder.AddTableStation().Name(tablenName).Type(TableType);
            tablebilder.AddTableProperty()
                .Name(TablePropertyName)
                .Type(TablePropertyType)
                .Value((_stationBilder.Station.Tables.Count).ToString(CultureInfo.InvariantCulture));
            return tablebilder;  
           
        } 

        public Dictionary<string, TampleBilder> ReadTamplates(TableBilder readTable, string sheetname, TepOrManual tep)
        {
            var dictionary = new Dictionary<string, TampleBilder>();
            var sheet = _wBook.Worksheet(sheetname);
            var tamplecells = sheet.Range(sheet.Row(2).Cell(6), sheet.Row(2).LastCellUsed());
            string tType = TampleType1;
            if (tep == TepOrManual.Tep) tType = TampleType2; 
            tamplecells.Cells().ForEach(cell =>
            {                                                          
                string tamplename = cell.GetString().Trim();
                var tample = readTable.AddTample().Name(tamplename).Type(tType);
                if (tep == TepOrManual.Tep) tample.AddTampleProperty()
                    .Name(TamplePropertyName)
                    .Type(TamplePropertyType)
                    .Value(TamplePropertyValue);

                tample.AddTampleProperty()
                    .Name(TampleProperty2Name)
                    .Type(TampleProperty2Type)
                    .Value((readTable.Table.Tamples.Count).ToString(CultureInfo.InvariantCulture));
                dictionary.Add(tamplename, tample);
            });

            return dictionary;

        }

        public Station ReadSignals(Dictionary<string, TampleBilder> tamples, string sheetname, TepOrManual tep)
        {
			Contract.Requires (tamples != null);
			var tmp = tamples.ToList ();
			string typesig = SignalType1;
			if (tep == TepOrManual.Tep)
				typesig = SignalType2;
            var sheet = _wBook.Worksheet(sheetname);
            var workcells = sheet.Range(sheet.Row(3).FirstCell(), sheet.LastCellUsed());
            ReadRows (tmp, typesig, workcells);
            return _stationBilder;
        }

		private void ReadRows (List<KeyValuePair<string, TampleBilder>> tmp, string typesig, IXLRange workcells)
		{
			foreach (var row in workcells.Rows ()) {
				for (int i = 0; i <= tmp.Count; i++) {
					if (row.Cell (i + 6).GetString ().Contains ("+")) {
						//заносим paramvalue
						_stationBilder.AddParameterValue ().Code (Replace(row.Cell (3).GetString ().Trim(),tmp[i].Key).Trim ('$'));
						//записываем сигнал
						var signal = tmp [i].Value.AddSignal ().Name (row.Cell (2).GetString ().Trim ()).Type (typesig);				
						signal.AddSignalProperty ().Name (SignalProp1Name).Type (SignalPropType).Value (Replace(row.Cell (3).GetString ().Trim(),tmp[i].Key).Trim ('$'));
						signal.AddSignalProperty ().Name (SignalProp11Name).Type (SignalPropType).Value (Replace(row.Cell (4).GetString ().Trim(),tmp[i].Key));
						signal.AddSignalProperty ().Name (SignalProp2Name).Type (SignalPropType).Value (row.Cell (1).GetString ().Trim ());
						signal.AddSignalProperty ().Name (SignalProp3Name).Type (SignalPropType).Value (tmp [i].Value.Tample.Signals.Count.ToString ());
					}
				}
			}
		}

		private string Replace(string formula,string tmpname)
		{
			var pattern = @"([\$])(.*?)([\$])";
			return Regex.Replace (formula, pattern, "${1}"+"${2}," + tmpname + ",сут" + "${3}");
		}

	public Station BildTable (string stationName,string sheetname, TepOrManual tep)
	{
		CreateStation (stationName);
		var tbl = ReadTable (sheetname);
		var tmpls = ReadTamplates (tbl, sheetname, tep);
		ReadSignals (tmpls, sheetname, tep);
		return _stationBilder;
	}

	public Dictionary<string, int> GetDictionarySheets()
	{
		var dictionary = new Dictionary<string, int>();
		int i = 0;
		_wBook.Worksheets.ForEach(w =>
			{
				dictionary.Add(w.Name,i);
				i++;
			});
		return dictionary;
	} 
    }
}
