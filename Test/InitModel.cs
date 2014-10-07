using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlConvertForIstok.Convert;

namespace Test
{
    static class InitModel
    {
        public static Station Station = new Station
        {
            Name = "НТЭЦ4",
            Property = new List<Property> { new Property { Name = "sortindex1", Type = "string", Value = "1" }},
            Type = "Station",
            ParameterValueses = new List<ParameterValues>
            {
                new ParameterValues{Code = "parvaluecode",Value = new Value{ChangeTimer = "27.12.2013 22:07:44",Quality = "Good",Time = "01.01.2012 0:00:00",Val = "180"}},
                new ParameterValues{Code = "parvaluecode2"}
            },
            Tables = new List<Table>
            {
                new Table
                {
                    Name = "Ручной ввод, турбины «вахта»",
                    Type = "Folder",
                    Property = new List<Property>{new Property{Name = "sortindex2", Type = "string", Value = "2"}, new Property { Name = "Testprop", Type = "string", Value = "1" } },
                    Tamples = new List<Tample>
                    {
                        new Tample
                        {
                            Type = "TEPTemplate",
                            Name = "2 очередь",
                            Property = new List<Property>{new Property{Name = "interval", Type = "string", Value = "[UTC+04]=1d"}},
                            Signals = new List<Signal>
                            {
                                new Signal
                                {
                                    Name = "Расход тепла на СН ТО",
                                    Type = "ManualParameter",
                                    Property = new List<Property>
                                    {
                                        new Property {Name = "formula_cod", Type = "string", Value = "Q^{в}_тсн"},
                                        new Property {Name = "index", Type = "string", Value = "1.9.24"},
                                        new Property {Name = "sortindex", Type = "string", Value = "4"}
                                    }
                                }
                            }
                        },
                        new Tample
                        {
                            Type = "TEPTemplate",
                            Name = "3 очередь",
                            Property = new List<Property>{new Property{Name = "interval", Type = "string", Value = "[UTC+04]=1d"}},
                            Signals = new List<Signal>
                            {
                                new Signal
                                {
                                    Name = "Расход тепла на СН ТО",
                                    Type = "ManualParameter",
                                    Property = new List<Property>
                                    {
                                        new Property {Name = "formula_cod", Type = "string", Value = "Q^{в}_тсн"},
                                        new Property {Name = "index", Type = "string", Value = "1.9.24"},
                                        new Property {Name = "sortindex", Type = "string", Value = "4"}
                                    }
                                }
                            }
                        }

                    }
                }
            }

        };
    }
}
