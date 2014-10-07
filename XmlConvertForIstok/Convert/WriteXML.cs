using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DocumentFormat.OpenXml.Math;

namespace XmlConvertForIstok.Convert
{
    public class WriteXml
    {
        private const string XmlElement = "node";
        private const string XmlProperty = "property";

        private const string XmlParameterValueEl = "parameter_values";
        private const string XmlParameterValueCode = "code";
        private const string XmlValueElem = "value";
        private const string XmlValueTime = "time";
        private const string XmlValueChange = "change_time";
        private const string XmlValueQuality = "quality";

        private const string XmlAttributeName = "name";
        private const string XmlAttributeType = "type";

      
        private readonly Station _station;
        private readonly string _filename;
        private XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "да"));
        public WriteXml(Station station, string filename)
        {
            _station = station;
            _filename = filename;
            
        }

        public void WriteXmlMethod()
        {
            
            var str = new XElement("InfoSysStructure");
            var station = new XElement(XmlElement, new XAttribute(XmlAttributeName, _station.Name), new XAttribute(XmlAttributeType, _station.Type));
            _station.Property.ForEach(stationprop => station.Add(new XElement(XmlProperty, new XAttribute(XmlAttributeName, stationprop.Name), new XAttribute(XmlAttributeType, stationprop.Type), stationprop.Value)));
            
            //для каждой таблицы в станции
            _station.Tables.ForEach(table =>
            {
                var xmlTable = new XElement(XmlElement, new XAttribute(XmlAttributeName, table.Name),new XAttribute(XmlAttributeType, table.Type));
                table.Property.ForEach(tableprop => xmlTable.Add(new XElement(XmlProperty, new XAttribute(XmlAttributeName, tableprop.Name), new XAttribute(XmlAttributeType, tableprop.Type), tableprop.Value)));
                //для каждого шаблона в таблице
                table.Tamples.ForEach(tamplet =>
                {
                    var xmlTamp = new XElement(XmlElement, new XAttribute(XmlAttributeName, tamplet.Name),new XAttribute(XmlAttributeType, tamplet.Type));
                    tamplet.Property.ForEach(tampletprop => xmlTamp.Add(new XElement(XmlProperty, new XAttribute(XmlAttributeName, tampletprop.Name), new XAttribute(XmlAttributeType, tampletprop.Type), tampletprop.Value)));
                    //для каждого сигнала в шаблоне
                    tamplet.Signals.ForEach(signal =>
                    {
                        var xmlSignal = new XElement(XmlElement, new XAttribute(XmlAttributeName, signal.Name), new XAttribute(XmlAttributeType, signal.Type));
                        //для каждого свойства сигнала
                        signal.Property.ForEach(property =>
                        {
                            var xmlProperty = new XElement(XmlProperty, new XAttribute(XmlAttributeName, property.Name), new XAttribute(XmlAttributeType, property.Type), property.Value);
                            //свойства в сигналы
                            xmlSignal.Add(xmlProperty);
                        });
                        //сигналы в шаблон
                        xmlTamp.Add(xmlSignal);
                    });
                    //шаблоны в таблицу
                    xmlTable.Add(xmlTamp);
                });
                //таблицу в станцию
                station.Add(xmlTable);
            });
            //станцию в общий файл
            str.Add(station);

            //добавляем все parameter_values
            _station.ParameterValueses.ForEach(parvalue =>
            {
                var xmlParValue = new XElement(XmlParameterValueEl, new XAttribute(XmlParameterValueCode, parvalue.Code));
                //если в значении есть value
                if (parvalue.Value != null)
                    xmlParValue.Add(new XElement(XmlValueElem, 
                        new XAttribute(XmlValueTime, parvalue.Value.Time),
                        new XAttribute(XmlValueChange, parvalue.Value.ChangeTimer),
                        new XAttribute(XmlValueQuality, parvalue.Value.Quality), parvalue.Value.Val));
                str.Add(xmlParValue);
            });
            doc.Add(str);
            doc.Save(_filename);
        }

        public bool WriteXmlbySerealise()
        {
            var xmlser = new XmlSerializer(_station.GetType()); 
            var writer = new StreamWriter(_filename);
            xmlser.Serialize(writer, _station);
            writer.Flush();
            writer.Close();  
            return File.Exists(_filename); 
        }
    }
}
