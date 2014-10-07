using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlConvertForIstok.Convert;

namespace Test
{
    [TestClass]
    public class TexReadTest
    {
        
        private string _filename;

        public TexReadTest()
        {
             _filename = "TexTest.xlsx"; 
        }
        [TestMethod]
        public void OpenBook()
        {
            var station = Station.Create();
            var book = new ExcelTexRead(station, _filename); 
            Assert.IsNotNull(book);
        }
        [TestMethod]
        public void ReadStationTest()
        {
            var station = Station.Create();
            var book = new ExcelTexRead(station, _filename);
            

            book.CreateStation("StationName");
            string stationName = station.Station.Name;

            StringAssert.Contains(stationName, "StationName");
        }

        [TestMethod]
        public void ReadTableTest()
        {
            var station =  Station.Create();
            var book  = new ExcelTexRead(station, _filename);
            string sheetname = "Лист1";  

            string tablename = book.ReadTable(sheetname).Table.Name; 

            StringAssert.Contains(tablename, "Расчет номинальных показателей турбоагрегата, группы турбоагрегатов");
        }
        [TestMethod]
        public void ReadTampleTest()
        {
            var station = Station.Create();
            var book = new ExcelTexRead(station, _filename);
            string sheetname = "Лист1";

            Dictionary<string, TampleBilder> tampleBilders = book.ReadTamplates(book.ReadTable(sheetname),sheetname,TepOrManual.Tep);


            Assert.IsTrue(tampleBilders.Count == 8);
            Assert.IsNotNull(tampleBilders["3"]);
            StringAssert.Contains(tampleBilders["3"].Tample.Type, "TEPTemplate");
        }

        [TestMethod]
        public void ReadValueProperties()
        {
            var station = Station.Create();
            var book = new ExcelTexRead(station, _filename); 
            string sheetname = "Лист1";
            var tamples =   book.ReadTamplates(book.ReadTable(sheetname),sheetname,TepOrManual.Tep);

            Station st = book.ReadSignals(tamples, sheetname, TepOrManual.Tep);

            StringAssert.Contains(st.ParameterValueses.First().Code, "$Э^к_i$"); 
            Assert.IsTrue(st.ParameterValueses.Count == 151);

            

        }
    }
}
