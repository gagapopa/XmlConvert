using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlConvertForIstok.Convert;

namespace Test
{
    [TestClass]
    public class ReadWordTest
    { 
        [TestMethod]
        public void ReadParameterValues()
        {
            var _station = Station.Create();
            var  app = new ReadWord("TestExcel.xlsx");
            var assertparams = app.ReadParameterValues(_station, 0).ParameterValueses;
            StringAssert.Contains(assertparams.First().Code, "D^{1}_{пе}");
            StringAssert.Contains(assertparams[5].Code, "O^{сп}_{2}");
            

        }

        [TestMethod]
        public void ReadParameterValuesNodes()
        {
            var _station = Station.Create();
            var app = new ReadWord("TestExcel.xlsx"); 
            var assertnodes = app.ReadParameterValues(_station, 0);

            StringAssert.Contains(assertnodes.Tables[0].Name, "Автоматизированные системы сбора и обработки данных по котлам");
            StringAssert.Contains(assertnodes.Tables[0].Property[0].Value, "0");
            StringAssert.Contains(assertnodes.Tables[0].Property[0].Name, "sortindex");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Type, "ManualGate");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Name, "Расход перегретого пара, 1 нитка");
        }
        //[TestMethod]
        //public void ReadParameterValuesKoef()
        //{
        //    var _station = Station.Create();
        //    var app = new ReadWord("TestExcel.xlsx");
        //    var assertnodes = app.ReadParameterValues(_station, 2);

        //    StringAssert.Contains(assertnodes.Tables[0].Name, "Автоматизированные системы сбора и обработки данных по ТЭЦ АСКУТЭ");
        //    StringAssert.Contains(assertnodes.Tables[0].Type, "Folder");
        //    StringAssert.Contains(assertnodes.Tables[0].Property[0].Value, "0");
        //    StringAssert.Contains(assertnodes.Tables[0].Property[0].Name, "sortindex");

        //    StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Type, "ManualGate");
        //    StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Name, "тэц");

        //    StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Name, "№яч.66 – СН-3");
        //    StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Property[0].Value, "k^{тфуII}_{66}");
        //}

        [TestMethod]
        public void ReadFormula()
        {
            var _station = Station.Create();
            var app = new ReadWord("TestExcel.xlsx");
            var assertnodes = app.ReadFormula(_station, 5);

            StringAssert.Contains(assertnodes.Tables[0].Name, "Расчет ТЭП, котлы, «вахта»");
            StringAssert.Contains(assertnodes.Tables[0].Type, "Folder");

            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Name, "к-5");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Type, "TEPTemplate");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Property[0].Value, "[UTC+04]=1d");

            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Name, "Признак работы");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Property[0].Value, "k^{в}_{к}");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Property[0].Name, "formula_cod");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Property[1].Name, "formula_text");
            //StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Property[1].Value, "$k^{тоII}_{80}$*(first($э^{тоII}_{80}$)-first($э^{тоII}_{80}$,1))/1e3");

            StringAssert.Contains(assertnodes.ParameterValueses[0].Code, "k^{в}_{к}");
            
        }

        [TestMethod]
        public void readValuesManyTamples()
        {
            var _station = Station.Create();
            var app = new ReadWord("TestExcel.xlsx");
            var assertnodes = app.ReadParameterValues(_station, 0);

            StringAssert.Contains(assertnodes.Tables[0].Name, "Автоматизированные системы сбора и обработки данных по котлам");
            StringAssert.Contains(assertnodes.Tables[0].Type, "Folder");

            
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Type, "ManualGate");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Name, "к-9");

            //StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Property[2].Value, "0");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Name, "Расход перегретого пара, 1 нитка");
            StringAssert.Contains(assertnodes.Tables[0].Tamples[0].Signals[0].Property[0].Value, "D^{1}_{пе}");

            
        }
        [TestMethod]
        public void GetDictionarySheets()
        {
            var app = new ReadWord("TestExcel.xlsx");
            var dic = app.GetDictionarySheets();
            Assert.IsTrue(dic["рп1.1"] == 0);

        }


        [TestMethod]
        public void Bilder()
        {
            var _station = Station.Create().NameStation("stat");
            _station.AddPropertyStation().Name("one");
            _station.AddPropertyStation().Name("two");
            var param = _station.AddParameterValue();
            param.Code("code").AddValue().ChangeTimer("change").Quality("qual").Time("time");
            _station.AddParameterValue().Code("code2");
            Station s = _station;
            Console.WriteLine(s.Property.First().Name);

        }

        
        //}

    }
}
