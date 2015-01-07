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
		public void AddNode()
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
		public void AddProperty()
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
	}	
}
