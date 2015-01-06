/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 06.01.2015
 * Время: 15:18
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using NUnit.Framework;
using XmlConvertForIstok.Composite;

namespace SecondTest
{
	[TestFixture]
	public class CompositeModelTesting
	{
		[Test]
		public void CreateNode()
		{
			var node = new Node();
			node.tagName = "TestTagName";
			node.Name	= "testName";
			node.Type = "testtype";			
		}
		
		[Test]
		public void CreateProperty()
		{
			var prop = new PropertyNode();			
			prop.tagName = "TestTagName";
			prop.Name	= "testName";
			prop.Type = "testtype";
			prop.Text = "TestText";			
		}
		
		[Test]
		public void AddNode()
		{
			var nodeone = new Node();
			nodeone.tagName = "TestTagNameone";
			var nodetwo = new Node();
			nodetwo.tagName = "TestTagNametwo";
			
			nodeone.Nodes.Add(nodetwo);
			
			Assert.IsNotNull(nodeone.Nodes.Find(n => n.tagName == "TestTagNametwo"));						
		}	
		
		[Test]
		public void AddPropertyNode()
		{
			var nodeone = new Node();
			nodeone.tagName = "TestTagNameone";
			var prop = new PropertyNode();
			prop.Text = "TestText";
			
			nodeone.Nodes.Add(prop);
			
			Assert.IsNotNull(nodeone.Nodes[0] as PropertyNode);
			Assert.IsTrue(((PropertyNode)nodeone.Nodes[0]).Text == "TestText");
		}
	}
}
