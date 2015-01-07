/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 06.01.2015
 * Время: 21:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace XmlConvertForIstok.Composite
{
	/// <summary>
	/// Билдер для заполнения модели.
	/// </summary>
	public class NodeBilder : INodeBilder
	{
		private Node node;	
		public NodeBilder()
		{
			node = new Node();
			node.tagName = "InfoSysStructure";
		}
		public NodeBilder(Node node)
		{
			this.node = node;			
		}
	#region INodeBilder implementation
	
	public INodeBilder AddNode(string name, string type)
	{
		Node newnode = new Node(){
			Name = name,
			Type = type,
			tagName = "node"
		};
		node.Nodes.Add(newnode);
		return new NodeBilder(newnode);
	}
	
	public void AddProperty(string name, string text)
	{
		PropertyNode prop = new PropertyNode(){
			Name = name,
			Type = "string",
			tagName = "property",
			Text = text
		};
		node.Nodes.Add(prop);		
	}
	#endregion
	
	public static implicit operator Node(NodeBilder nodebilder){		
		return nodebilder.node;	}
		
	}
}
