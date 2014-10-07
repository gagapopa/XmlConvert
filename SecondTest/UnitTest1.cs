using System;
using XmlConvertForIstok.Composite;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecondTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddingComponent()
        {
            const string name = "testname";
            IMyComposite composite = new Composite();
            IMyComponent nodeComponent = new TableComponent { Name = name };
           
            composite.AddComponent(nodeComponent);
            IMyComponent comp = composite.FindChild(name);

            StringAssert.Contains(comp.Name, name);
        }
    }

   
}
