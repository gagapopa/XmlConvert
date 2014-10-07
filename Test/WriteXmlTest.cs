using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Annotations;
using XmlConvertForIstok.Convert;
using System.Xml.Linq;

namespace Test
{
    [TestClass]
    public class WriteXmlTest
    {
        Station _station = InitModel.Station;
        const string fileName = "Testxml";
        private readonly XDocument doc;
        private WriteXml xmlwr;
        public WriteXmlTest()
        {
             xmlwr = new WriteXml(_station, fileName);
             xmlwr.WriteXmlMethod();
             doc = XDocument.Load(fileName);
             File.Delete(@"C:\Users\Господин\Documents\visual studio 2013\Projects\XmlConvertForIstok\Test\bin\Debug\Testxml");
        }

        [TestMethod]
        public void TestElements()
        {
            var station = doc.Root.Element("node");
            var table = station.Elements("node").First();
            var template = table.Elements("node").ToList();
            var signal = template[0].Elements("node").First();
            var sigsprop = signal.Elements("property").ToList();


            Assert.IsTrue(station.Attribute("name").Value == "НТЭЦ4");
            Assert.IsTrue(table.Attribute("name").Value == "Ручной ввод, турбины «вахта»");
            Assert.IsTrue(template[0].Attribute("name").Value == "2 очередь");
            Assert.IsTrue(template[1].Attribute("name").Value == "3 очередь");
            Assert.IsTrue(signal.Attribute("name").Value == "Расход тепла на СН ТО");
            Assert.IsTrue(sigsprop[0].Attribute("name").Value == "formula_cod" && sigsprop[1].Attribute("name").Value == "index");
        }

        [TestMethod]
        public void TestProperty()
        {
            var station = doc.Root.Element("node");
            var table = station.Elements("node").ToList();
            var template = table[0].Elements("node").ToList();
            var signal = template[0].Elements("node").First();
            

            Assert.IsTrue(station.Element("property").Attribute("name").Value == "sortindex1");
            Assert.IsTrue(table[0].Element("property").Attribute("name").Value == "sortindex2");
            Assert.IsTrue(table[0].Elements("property").ToList()[1].Attribute("name").Value == "Testprop");
            Assert.IsTrue(template[0].Element("property").Attribute("name").Value == "interval");
            Assert.IsTrue(signal.Element("property").Attribute("name").Value == "formula_cod");

        }

        [TestMethod]
        public void TestParValue()
        {
            var parvalue = doc.Root.Elements("parameter_values").First();
            var value = parvalue.Elements("value").First();

            Assert.IsTrue(parvalue.Attribute("code").Value == "parvaluecode");
            Assert.IsTrue(value.Attribute("change_time").Value == "27.12.2013 22:07:44");
        }

        [TestMethod]
        public void TestSerealise()
        {
            var forser = new WriteXml(_station, "TestSerealise.xml");
            forser.WriteXmlbySerealise();
            Assert.IsTrue(File.Exists("TestSerealise.xml"));

        }
    }
}
