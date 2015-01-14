/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 06.01.2015
 * Время: 21:26
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using NUnit.Framework;
using System.IO;
using XmlConvertForIstok.Composite;

namespace SecondTest
{
	[TestFixture]
	public class NodeBilderTest
	{
		[Test]
		public void InitBilder()
		{
			INodeBilder node = new NodeBilder();
			
			Node subnode = (NodeBilder)node;
			
			Assert.IsNotNull(subnode);
			StringAssert.Contains("InfoSysStructure", subnode.tagName);
		}
		
		[Test]
		public void AddNodeTest()
		{
			INodeBilder node = new NodeBilder();
			
			INodeBilder subbilder = node.AddNode("1nodename", "1nodetype");
			Node assertnode = (NodeBilder)node;
			Node assertsubbilder = (NodeBilder)subbilder;
			
			Assert.IsTrue(assertnode.Nodes.Count == 1);
			StringAssert.Contains("1nodename", assertsubbilder.Name);
			StringAssert.Contains("1nodetype", assertsubbilder.Type);
			StringAssert.Contains("node", assertsubbilder.tagName);
		}	
		
		[Test]
		public void AddPropertyTest()
		{
			INodeBilder node = new NodeBilder();
			
			node.AddProperty("1PropertyName", "1PropertyText");
			Node assertNode = (NodeBilder)node;
			PropertyNode prop = assertNode.Nodes[0] as PropertyNode;
			
			Assert.IsNotNull(prop);
			StringAssert.Contains(prop.Name, "1PropertyName");
			StringAssert.Contains(prop.Text, "1PropertyText");
			StringAssert.Contains(prop.Type, "string");
			StringAssert.Contains(prop.tagName, "property");
		}
		
		[Test]
		public void AddParamValuesTest()
		{
			INodeBilder node = new NodeBilder();
			
			node.AddParamValues("code");
			Node assertNode = (NodeBilder)node;
			ParamValuesNode param = assertNode.Nodes[0] as ParamValuesNode;
			
			Assert.IsNotNull(param);
			StringAssert.Contains("code", param.Code);			
		}
		
		[Test]
		public void XmlSrialyzerTest()
		{
			INodeBilder node = new NodeBilder();
			
			const string Filename = @"testfile";
			if (File.Exists(Filename + "xml")) 
			{
				File.Delete(Filename + "xml");
			}
			
			node.AddNode("node1", "type1").AddProperty("prop1", "proptext1")
				.AddNode("node2", "type2").AddProperty("prop2", "proptext2");
			node.AddParamValues("code");
			node.Serialyze(Filename);
			
			Assert.IsTrue(File.Exists(Filename + ".xml"));
		}
		
		[Test]
		public void GetNodesNumberTest()
		{
			INodeBilder node = new NodeBilder();
			
			node.AddNode("node1", "type1").AddProperty("prop1", "proptext1");
			node.AddNode("node2", "type2").AddProperty("prop2", "proptext2");
			node.AddProperty("prop3", "proptext3");			
			
			int nodeNumber = node.GetNodesNumber();
			
			Assert.IsTrue(nodeNumber == 2);
		}
	}	
}
