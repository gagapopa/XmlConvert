using NUnit.Framework;
using XmlConvertForIstok.Convert;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MonoTest
{
	[TestFixture ()]
	public class Test
	{
		private string _filename;

		public Test()
		{
			_filename = "TexTest.xlsx"; 
		}

		[Test ()]
		public void OpenBook()
		{
			var station = Station.Create();
			var book = new ExcelTexRead(station, _filename); 
			Assert.IsNotNull(book);
		}
		[Test ()]
		public void ReadStationTest()
		{
			var station = Station.Create();
			var book = new ExcelTexRead(station, _filename);


			book.CreateStation("StationName");
			string stationName = station.Station.Name;
			var stproperty = station.Station.Property.First ();

			StringAssert.Contains(stationName, "StationName");
			StringAssert.Contains(stproperty.Name, "sortindex");
			StringAssert.Contains(stproperty.Type, "string");
			StringAssert.Contains(stproperty.Value, "1");
		}

		[Test ()]
		public void ReadTableTest()
		{
			var station =  Station.Create();
			var book  = new ExcelTexRead(station, _filename);
			string sheetname = "Лист1";  

			string tablename = book.ReadTable(sheetname).Table.Name; 

			StringAssert.Contains(tablename, "Расчет номинальных показателей турбоагрегата, группы турбоагрегатов");
		}
		[Test ()]
		public void ReadTampleTest()
		{
			var station = Station.Create();
			var book = new ExcelTexRead(station, _filename);
			string sheetname = "Лист1";

			Dictionary<string, TampleBilder> tampleBilders = book.ReadTamplates(book.ReadTable(sheetname),sheetname,TepOrManual.Tep);


			Assert.IsTrue(tampleBilders.Count == 8);
			Assert.IsNotNull(tampleBilders["т-3"]);
			StringAssert.Contains(tampleBilders["т-3"].Tample.Type, "TEPTemplate");
		}

		[Test ()]
		public void ReadValueProperties()
		{
			var station = Station.Create();
			var book = new ExcelTexRead(station, _filename); 
			string sheetname = "Лист1";
			var tamples =   book.ReadTamplates(book.ReadTable(sheetname),sheetname,TepOrManual.Tep);

			Station st = book.ReadSignals(tamples, sheetname, TepOrManual.Tep);

			StringAssert.Contains(st.ParameterValueses.First().Code, "Э^к_i,т-3,сут"); 
		}
		[Test ()]
		public void ReadSignals()
		{
			var station = Station.Create();
			var book = new ExcelTexRead(station, _filename); 
			string sheetname = "Лист1";

			var tamples =   book.ReadTamplates(book.ReadTable(sheetname),sheetname,TepOrManual.Tep);

			Station st = book.ReadSignals(tamples, sheetname, TepOrManual.Tep);
			var signals = st.Tables.First ().Tamples.First ().Signals;

			StringAssert.Contains (st.Tables.First ().Tamples.First ().Name, "т-3");
			StringAssert.Contains (signals.First().Name, " Выработка электроэнергии турбоагрегатом в конденсационном режиме, тыс.кВтч");
			StringAssert.Contains (signals.First ().Type, "TEP");
			StringAssert.Contains (signals.First ().Property [0].Name, "formula_cod");
			StringAssert.Contains (signals.First ().Property [1].Name, "formula_text");
			StringAssert.Contains (signals.First ().Property [2].Name, "index");
			StringAssert.Contains (signals.First ().Property [3].Name, "sortindex");
			//StringAssert.Contains (signals.First ().Property [4].Name, "unit");$
			StringAssert.Contains (signals.First ().Property [0].Value, "Э^к_i,т-3,сут");
			StringAssert.Contains (signals.First ().Property [1].Value, "if($r_т,т-3,сут$==1) return $Э,т-3,сут$;");
			StringAssert.Contains (signals.First ().Property [2].Value, "8.1.2.");
		}

		[Test ()]
		public void BildModel()
		{
			var station = Station.Create();
			var book = new ExcelTexRead(station, _filename); 
			string sheetname = "Лист1";
			var stationName ="НТЭЦ4";

			Station st = book.BildTable (stationName, sheetname, TepOrManual.Tep);
			var signals = st.Tables.First ().Tamples.First ().Signals;


			Assert.IsTrue(st.Tables.First().Tamples.Count == 8);
			StringAssert.Contains(st.Tables.First().Tamples.First().Type, "TEPTemplate");

			StringAssert.Contains(st.ParameterValueses.First().Code, "Э^к_i,т-3,сут");

			StringAssert.Contains (st.Tables.First ().Tamples.First ().Name, "т-3");  
			StringAssert.Contains (signals.First().Name, " Выработка электроэнергии турбоагрегатом в конденсационном режиме, тыс.кВтч");
			StringAssert.Contains (signals.First ().Type, "TEP");
			StringAssert.Contains (signals.First ().Property [0].Name, "formula_cod");
			StringAssert.Contains (signals.First ().Property [1].Name, "formula_text");
			StringAssert.Contains (signals.First ().Property [2].Name, "index");
			StringAssert.Contains (signals.First ().Property [3].Name, "sortindex");
			//			StringAssert.Contains (signals.First ().Property [4].Name, "unit");
			StringAssert.Contains (signals.First ().Property [0].Value, "Э^к_i,т-3,сут");
			StringAssert.Contains (signals.First ().Property [1].Value, "if($r_т,т-3,сут$==1)[т-3]:$Э_{тг-3},т-3,сут$;if($r_т,т-3,сут$==1)[т-4]:$Э_{тг-4},т-3,сут$;if($r_т,т-3,сут$==1)[т-5]:$Э_{тг-5},т-3,сут$;if($r_т,т-3,сут$==1)[т-6]:$Э_{тг-6},т-3,сут$;if($r_т,т-3,сут$==1)[т-7]:$Э_{тг-7},т-3,сут$;if($r_т,т-3,сут$==1)[т-8]:$Э_{тг-8},т-3,сут$;");
			StringAssert.Contains (signals.First ().Property [2].Value, "8.1.2.");

		}

	}
}

