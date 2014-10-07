using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlConvertForIstok.Convert;
namespace Test
{
    [TestClass]
    public class AllTest
    {
        [TestMethod]
        public void AllOne()
        {
            var _station = Station.Create().TypeStation("Station").NameStation("НТЭЦ4");
            var app = new ReadWord("TestExcel.xlsx");
            app.ReadParameterValues(_station, 2);
            var station = app.ReadFormula(_station, 1);
            var write = new WriteXml(station, "Testxml.xml");
            write.WriteXmlbySerealise();
            Assert.IsTrue(File.Exists("Testxml.xml"));
        }

        [TestMethod]
        public void Getserialaze()
        {
            var ser = InitModel.Station;
            var xmlser = new XmlSerializer(ser.GetType());
            var writer = new StreamWriter("TestSerealise.xml");
            xmlser.Serialize(writer, ser);  
            writer.Flush();
            writer.Close();
    }
}
}
