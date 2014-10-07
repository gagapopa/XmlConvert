using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlConvertForIstok.Composite
{
    public class Composite : ModelComponent, IMyComposite
    { 
        private List<IMyComponent> _components = new List<IMyComponent>();
        public override IMyComponent FindChild(string name)
        {
            return Name == name ? this : _components.FirstOrDefault(found => found.Name == name);
        }
        public void AddComponent(IMyComponent myComponent)
        {
            _components.Add(myComponent);
        }

        
    }
}
