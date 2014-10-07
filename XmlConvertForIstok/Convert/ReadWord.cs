using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
namespace XmlConvertForIstok.Convert
{
    public class ReadWord
    {
        private XLWorkbook wb;
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

        private const string SignalType = "ManualParameter";
        private const string SignalProp1Name = "formula_cod";
        private const string SignalProp2Name = "index";
        private const string SignalProp3Name = "sortindex";
        private const string SignalPropType = "string";

        public ReadWord(string filename)
        {
            wb = new XLWorkbook(filename);
        }
        //считывание таблиц Показаний и Коэффициентов
        public Station ReadParameterValues(StationBilder station, int workSheet, string patternfordown = @"[_][\w,\(,\),\-,\,]*", string patternforup = @"[\^].*" )
        {
            Contract.Requires(workSheet >= 0, "отрицательный номер Листа");
            Contract.Requires(station != null, "stationBilder == null in ReadParameterValues");
            //выбираем лист в экселе
            var wh = wb.Worksheets.ToList()[workSheet];
            string tableName = wh.Cell(1, 1).GetString();
            //выделяем рабочие ячейки
            var workrange = wh.Range(wh.FirstCell().CellBelow(), wh.LastRowUsed().LastCellUsed())
                .AsTable()
                .DataRange
                .Rows();
            //проверяем наличие необходимых полей
            Contract.Assert(workrange.First().Field("Обозначение") != null, "Нет поля 'Обозначение'");
            Contract.Assert(workrange.First().Field("Показатель") != null, "Нет поля 'Показатель'");
            Contract.Assert(workrange.First().Field("№ п/п") != null, "Нет поля '№ п/п'");
            
            //создаем таблицу и ее свойства
            var table = station.AddTableStation();
            table.Name(tableName).Type(TableType)
                .AddTableProperty()
                .Name(TablePropertyName)
                .Type(TablePropertyType)
                .Value((station.Station.Tables.Count-1).ToString());
            //создаем шаблоны и словарь билдеров шаблонов
            var tampleDictionary = new Dictionary<string, TampleBilder>();
            var tamplecells = wh.Range(workrange.First().Field(4), workrange.First().LastCellUsed()).Cells().ToList();
            tamplecells.ForEach(c =>
            {
                var tname = c.WorksheetColumn().Cell(2).GetString().Trim();
                if (tname.Contains("Значение")) tname = TampleName;
                var tm = table.AddTample().Name(tname).Type(TampleType1);
                tm.AddTampleProperty().Name(TamplePropertyName).Type(TamplePropertyType).Value(TamplePropertyValue);
                tm.AddTampleProperty().Name(TampleProperty2Name).Type(TampleProperty2Type).Value((table.Table.Tamples.Count-1).ToString());
                tampleDictionary.Add(tname, tm);
            });
            //Составляем список параметров 
            var paramlist = workrange.Select(s => new StringBuilder(s.Field("Обозначение").GetString())).ToList();
            //список отредактированных обозначений параметров
            var validParams = GetValidParams(paramlist, patternfordown, patternforup);
             //отредактированный список параметров заносим в модель 
            validParams.ForEach(p => Contract.Requires(!validParams.HasDuplicates(),"Повторяющиеся значения в Обозначениях"));
            //проходим по строкам и вносим значения в нашу модель
            int i = 0;
            workrange.ForEach(row =>
            {
                var peremen = validParams[i].ToString().Trim();
                var name = row.Field("Показатель").GetString().Trim();
                var id = row.Field("№ п/п").GetString().Trim();
                var val = row.Field(5).GetString().Trim();
                var par = station.AddParameterValue();
                
                if (tampleDictionary.Keys.Contains(TampleName) && val.Contains("+"))
                {
                    var sig = tampleDictionary[TampleName].AddSignal();
                    par.Code(peremen);
                    sig.Name(name).Type(SignalType);
                    sig.AddSignalProperty().Name(SignalProp1Name).Type(SignalPropType).Value(peremen);
                    sig.AddSignalProperty().Name(SignalProp2Name).Type(SignalPropType).Value(id);
                    sig.AddSignalProperty().Name(SignalProp3Name).Type(SignalPropType).Value((tampleDictionary[TampleName].Tample.Signals.Count - 1).ToString());

                }
                else if (tampleDictionary.Keys.Contains(TampleName) && !val.Contains("+"))
                {
                    var sig = tampleDictionary[TampleName].AddSignal();
                    par.Code(peremen).AddValue()
                        .ChangeTimer(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString())
                        .Quality("Good")
                        .Time("01.01.2012 0:00:00");
                    sig.Name(name).Type(SignalType);
                    sig.AddSignalProperty().Name(SignalProp1Name).Type(SignalPropType).Value(peremen);
                    sig.AddSignalProperty().Name(SignalProp2Name).Type(SignalPropType).Value(id);
                    sig.AddSignalProperty().Name(SignalProp3Name).Type(SignalPropType).Value((tampleDictionary[TampleName].Tample.Signals.Count - 1).ToString());
                }
                
                else
                {
                    par.Code(peremen);
                    var cl = wh.Range(row.Field(4), row.LastCellUsed()).Cells(c => c.GetString().Contains("+"));
                    cl.ForEach(cell =>
                    {
                        var columnname = cell.WorksheetColumn().Cell(2).GetString().Trim();
                        Contract.Assert(tampleDictionary[columnname] != null, "Нет нужного шаблона в словаре шаблонов ReadParameterValues");
                        var sig = tampleDictionary[columnname].AddSignal().Name(name).Type(SignalType);
                        sig.AddSignalProperty().Name(SignalProp1Name).Type(SignalPropType).Value(peremen.ToString());
                        sig.AddSignalProperty().Name(SignalProp2Name).Type(SignalPropType).Value(id.ToString());
                        sig.AddSignalProperty().Name(SignalProp3Name).Type(SignalPropType).Value((tampleDictionary[columnname].Tample.Signals.Count - 1).ToString());
                    });
                }
                i++;
            });
            table.RemoveEmptyTamples();
                
			return station;
            //[\w,∆,\〖,\〗,\ ̅,^\(]*?_*?[\),\(,\w]*?[\^][\w,\(,\),\〖,\〗,\.,,\ ̅]*
        }

        public Station ReadFormula(StationBilder station, int workSheet, string patternfordown = @"[_][\w,\(,\),\-,\,]*",
            string patternforup = @"[\^].*",string patternForPreInformation = @"([\S]*)[\ ][\–]")
        {
            //[\S]*[\^][\S]*
            Contract.Requires(workSheet >= 0, "отрицательный номер Листа");
            Contract.Requires(station != null, "stationBilder == null in Readformula");
            Contract.Requires(patternForPreInformation != null, "patternForPreInformation != null");
            var preRegexp = new Regex(patternForPreInformation);
            //выбираем лист в экселе
            var wh = wb.Worksheets.ToList()[workSheet];
            string tableName = wh.Cell(1, 1).GetString();
            //выделяем рабочие ячейки
            var workrange = wh.Range(wh.FirstCell().CellBelow(), wh.LastRowUsed().LastCellUsed())
                .AsTable()
                .DataRange
                .Rows();
            //создаем таблицу
            var table = station.AddTableStation();
            table.Name(tableName).Type(TableType)
                .AddTableProperty()
                .Name(TablePropertyName)
                .Type(TablePropertyType)
                .Value((station.Station.Tables.Count-1).ToString());
            //создаем шаблоны и словарь билдеров шаблонов
            var tampleDictionary = new Dictionary<string, TampleBilder>();
            var tamplecells = wh.Range(workrange.First().Field(6), workrange.First().LastCellUsed()).Cells().ToList();
             tamplecells.ForEach(c =>
             {
                 var tname = c.WorksheetColumn().Cell(2).GetString().Trim();
                 var tm = table.AddTample().Name(tname).Type(TampleType2);
                 tm.AddTampleProperty().Name(TamplePropertyName).Type(TamplePropertyType).Value(TamplePropertyValue);
                 tampleDictionary.Add(tname, tm);
             });
            //проверяем наличие необходимых полей
            Contract.Assert(workrange.First().Field("Обозначение") != null, "Нет поля 'Обозначение'");
            Contract.Assert(workrange.First().Field("Показатель") != null, "Нет поля 'Показатель'");
            Contract.Assert(workrange.First().Field("№ п/п") != null, "Нет поля '№ п/п'");
            //проходимся по каждой строке ашей таблицы
            workrange.ForEach(row =>
            {
                //считываем поля значений имен и формул
                var index = row.Field(0).GetString().Trim();
                var name = row.Field(1).GetString().Trim();
                var unit = row.Field(2).GetString().Trim();
                var formulaCode = row.Field(3).GetString().Trim();
                var formulaText = new StringBuilder(row.Field(4).GetString().Trim());
                var preFormula = row.Field(5).GetString().Trim();//исходная информация
                //вычленяем из исходной информации только наши переменные и обозначение рассчитываемой величины все скидываем в rowparams
                var m = preRegexp.Matches(preFormula);
                List<StringBuilder> rowParams = (from Match match in m select new StringBuilder(match.Groups[1].Value)).ToList();
                rowParams.Add(new StringBuilder(formulaCode));
                //получаем список валидных значений и заменяем старые значения в исходной формуле на валидные(например $k^{тоII}_{80}$) 
                var rowValidParams = GetValidParams(rowParams, patternfordown, patternforup);
                for (int i = 0; i < rowParams.Count; i++)
                {
                    formulaText.Replace(rowParams[i].ToString(), "$"+rowValidParams[i].ToString()+"$");
                }
                //удаляем пробелы
                formulaText.Replace(" ", "");
                //смотрим сколько плюсиков дальше есть в строке и для каждого добавляем сигнал в нужный шаблон
                var cl = wh.Range(row.Field(6), row.LastCellUsed()).Cells(c => c.GetString().Contains("+")); 
                cl.ForEach(cell =>
                {
                    var columnname = cell.WorksheetColumn().Cell(2).GetString().Trim();
                    Contract.Assert(tampleDictionary[columnname] != null, "Нет нужного шаблона в словаре шаблонов ReadFormula");
                    var sig = tampleDictionary[columnname].AddSignal().Name(name).Type("TEP");
                    sig.AddSignalProperty().Name("formula_cod").Type(SignalPropType).Value(rowValidParams.Last().ToString());
                    sig.AddSignalProperty().Name("formula_text").Type(SignalPropType).Value(formulaText.ToString());
                    sig.AddSignalProperty().Name("index").Type(SignalPropType).Value(index);
                    sig.AddSignalProperty().Name("sortindex").Type(SignalPropType).Value((tampleDictionary[columnname].Tample.Signals.Count-1).ToString());
                    sig.AddSignalProperty().Name("unit").Type(SignalPropType).Value(unit);
                });
                //добавляем параметер
                station.AddParameterValue().Code(rowValidParams.Last().ToString());
            });
            table.RemoveEmptyTamples();
            
            return station;

        }

        public Dictionary<string, int> GetDictionarySheets()
        {
           var dictionary = new Dictionary<string, int>();
            int i = 0;
           wb.Worksheets.ForEach(w =>
           {
               dictionary.Add(w.Name,i);
               i++;
           });
            return dictionary;
        } 
 
       

        private static List<StringBuilder> GetValidParams(List<StringBuilder> parametList, string pattdown, string pattup)
        {
            var validParams = new List<StringBuilder>();
            parametList.ForEach(a => validParams.Add(new StringBuilder(a.ToString())));

            //перебираем список, производя подстановки типа ^{val} _{val}
            validParams.ForEach(param =>
            {
                //Проверяем на совпедение по регулярным выраженькам
                var downMatch = Regex.Match(param.ToString(), pattdown);
                var upMatch = Regex.Match(param.ToString(), pattup);
                //Ежели найден параметр в нижнем регистре
                if (downMatch.Success)
                {
                    var d = new StringBuilder(downMatch.Value);
                    //убираем подчеркивание из d
                    d.Remove(0, 1);
                    param.Remove(param.ToString().IndexOf('_'), downMatch.Length);
                    // обрезаем скобки и вставляем нижний регистр в конец параметра.
                    param.Append("_{" + d.ToString().TrimEnd(')').TrimStart('(') + "}");
                }
                //Ежели в верхнем
                if (upMatch.Success)
                {
                    var u = new StringBuilder(upMatch.Value);
                    u.Remove(0, 1);
                    param.Remove(param.ToString().IndexOf('^') + 1, upMatch.Length - 1);
                    param.Insert(param.ToString().IndexOf('^') + 1, "{" + u.ToString() + "}");
                } 
            });
            
            return validParams;
        } 

        
    }
}
