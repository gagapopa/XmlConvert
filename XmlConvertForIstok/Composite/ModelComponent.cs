using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlConvertForIstok.Composite
{
    public abstract class ModelComponent  : IMyComponent
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual IMyComponent FindChild(string name)
        {
            throw new NotImplementedException();
        }
    }
    public class TableComponent : ModelComponent
    {
    }
}
