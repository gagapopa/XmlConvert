using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlConvertForIstok.Composite
{
    public interface IMyComponent
    {
        string Name { get; set; }
        string Type { get; set; }
        IMyComponent FindChild(string name);
    }
    public interface IMyComposite : IMyComponent
    {
        void AddComponent(IMyComponent myComponent);
    }
}
