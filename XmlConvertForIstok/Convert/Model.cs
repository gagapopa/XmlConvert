using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlConvertForIstok.Convert
{
     
    public class ParameterValues
    {
        [XmlAttribute("code")]
        public string Code;
        [XmlElement("value",typeof(Value))]
        public Value Value;

    } 
    public class Value
    {
        [XmlAttribute("time")]
        public string Time;
        [XmlAttribute("change_time")]
        public string ChangeTimer;
        [XmlAttribute("quality")]
        public string Quality;
        [XmlText]
        public string Val;
    }
    
    [XmlRoot("node")] 
   public class Station
    {
       public Station()
       {
             ParameterValueses = new List<ParameterValues>();
             Property = new List<Property>();
             Tables = new List<Table>();
       }
        
       
        [XmlAttribute("name")]
       public string Name { get; set; }
        [XmlAttribute("type")]
       public string Type { get; set; }
        [XmlElement("property")]
       public List<Property> Property { get; set; }
        [XmlElement("node")]
       public List<Table> Tables { get; set; }
        [XmlElement("parameter_values")]
       public List<ParameterValues> ParameterValueses { get; set; }

       public static StationBilder Create()
       {
           return  new StationBilder(new Station());
       }
    }
   
    public class Table
    {
        public Table()
        {
            Property = new List<Property>();
            Tamples = new List<Tample>();
        }
        [XmlAttribute("name")]
        public string Name { get; set; }
         [XmlAttribute("type")]
        public string Type { get; set; }
         [XmlElement("property")]
        public List<Property> Property { get; set; }
         [XmlElement("node")]
        public List<Tample> Tamples { get; set; }
    }
    public class Tample
    {
        public Tample()
        {
           Property = new List<Property>();
           Signals = new List<Signal>();
        }
        [XmlAttribute("name")]
        public string Name { get; set; }
         [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlElement("property")]
        public List<Property> Property { get; set; }
         [XmlElement("node")]
        public List<Signal> Signals { get; set; }
    }
   

    public class Signal
    {
        public Signal()
        {
            Property = new List<Property>();
        }
        [XmlAttribute("name")]
        public string Name { get; set; }
         [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlElement("property")]
        public List<Property> Property { get; set; }
    }

    public class Property
    {
        [XmlAttribute("name")]
        public string Name;
         [XmlAttribute("type")]
        public string Type;
        [XmlText]
        public string Value;  
    }
}
