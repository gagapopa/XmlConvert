using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace XmlConvertForIstok.Convert
{
    public class StationBilder
    {
        public Station Station { get; private set; }

        public StationBilder(Station station)
        {
            Station = station;   
        }

        public StationBilder NameStation(string name)
        {
            Station.Name = name;
            return this;
        }
        public StationBilder TypeStation(string type)
        {
            Station.Type = type;
            return this;
        }


        public PropertyBilder AddPropertyStation()
        {
            var prop = new Property();
            Station.Property.Add(prop);
            return new PropertyBilder(prop);
        }
        public ParameterValuesesBilder AddParameterValue()
        {
            var par = new ParameterValues();
            Station.ParameterValueses.Add(par);
            return new ParameterValuesesBilder(par);
        }

        public TableBilder AddTableStation()
        {
            var tbl = new Table();
            Station.Tables.Add(tbl);
            return new TableBilder(tbl);
        }   


        public static implicit operator Station(StationBilder bilder)
        {
            Contract.Requires(bilder != null);
            return bilder.Station;
        }
    }

    public class ParameterValuesesBilder
    {
        private ParameterValues _parameter;
        public ParameterValuesesBilder(ParameterValues parameter)
        {
            _parameter = parameter;
        }

        public ParameterValuesesBilder Code(string code)
        {
            _parameter.Code = code;
            return this;
        }

        public ValueBilder AddValue()
        {
            var val = new Value();
            return new ValueBilder(_parameter.Value = val);

        }
        public static implicit operator ParameterValues(ParameterValuesesBilder bilder)
        {
            Contract.Requires(bilder != null);
            return bilder._parameter;
        }
    }

    public class ValueBilder
    {
        private Value _value;

        public ValueBilder(Value value)
        {
            _value = value;
        }

        public ValueBilder Time(string time)
        {
            _value.Time = time;
            return this;
        }
        public ValueBilder ChangeTimer(string changeTimer)
        {
            _value.ChangeTimer = changeTimer;
            return this;
        }
        public ValueBilder Quality(string quality)
        {
            _value.Quality = quality;
            return this;
        }
        public ValueBilder Val(string val)
        {
            _value.Val = val;
            return this;
        }

    }

    public class TableBilder
    {

        public Table Table { get; private set; }

        public TableBilder(Table table)
        {
            Table = table;
        }

        public TableBilder Name(string name)
        {
            Table.Name = name;
            return this;
        }
        public TableBilder RemoveEmptyTamples()
        {
            var list = new List<string>();
            Table.Tamples.ForEach(tmp =>
            {
                if (tmp.Signals.Count == 0)
                {
                    list.Add(tmp.Name); 
                } 
            });
            list.ForEach(i =>
            {
                var tmpl = Table.Tamples.Find(a => a.Name == i);
                Table.Tamples.Remove(tmpl);
            });
            return this;
        }

        public TableBilder Type(string type)
        {
            Table.Type = type;
            return this;
        }

        public PropertyBilder AddTableProperty()
        {
            var prop = new Property();
            Table.Property.Add(prop);
            return new PropertyBilder(prop);
        }

        public TampleBilder AddTample()
        {
            var tmpl = new Tample();
            Table.Tamples.Add(tmpl);
            return new TampleBilder(tmpl);
        }
        public static implicit operator Table(TableBilder bilder)
        {
            Contract.Requires(bilder != null);
            return bilder.Table;
        }

    }

    public class TampleBilder
    {
        public Tample Tample { get; private set; }
        public TampleBilder(Tample tample)
        {
            Tample = tample;
        }

        public TampleBilder Name(string name)
        {
            Tample.Name = name;
            return this;
        }
        public TampleBilder Type(string type)
        {
            Tample.Type = type;
            return this;
        }
        public PropertyBilder AddTampleProperty()
        {
            var prop = new Property();
            Tample.Property.Add(prop);
            return new PropertyBilder(prop);
        }
        public SignalBilder AddSignal()
        {
            var signal = new Signal();
            Tample.Signals.Add(signal);
            return new SignalBilder(signal);
        } 

    }

    public class SignalBilder
    {
        private Signal _signal;

        public SignalBilder(Signal signal)
        {
            _signal = signal;
        }
        public SignalBilder Name(string name)
        {
            _signal.Name = name;
            return this;
        }
        public SignalBilder Type(string type)
        {
            _signal.Type = type;
            return this;
        }
        public PropertyBilder AddSignalProperty()
        {
            var prop = new Property();
            _signal.Property.Add(prop);
            return new PropertyBilder(prop);
        } 
    }

    public class PropertyBilder
    {
        private Property _property;
        public PropertyBilder(Property property)
        {
            _property = property;
        }

        public PropertyBilder Name(string name)
        {
            _property.Name = name;
            return this;
        }

        public PropertyBilder Type(string type)
        {
            _property.Type = type;
            return this;
        }

        public PropertyBilder Value(string value)
        {
            _property.Value = value;
            return this;
        }

        public static implicit operator Property(PropertyBilder bilder)
        {
            Contract.Requires(bilder != null);
            return bilder._property;
        }
    }
}
