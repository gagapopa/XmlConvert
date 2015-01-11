/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 06.01.2015
 * Время: 21:13
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

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
	
	public INodeBilder AddProperty(string name, string text)
	{
		PropertyNode prop = new PropertyNode(){
			Name = name,
			Type = "string",
			tagName = "property",
			Text = text
		};
		node.Nodes.Add(prop);
		return this;
	}
	public INodeBilder AddParamValues(string code)
	{
		ParamValuesNode param = new ParamValuesNode(){
			tagName = "parameter_values",
			Code = code
		};
		node.Nodes.Add(param);
		return this;
	}

	public bool Serialyze(string _filename)
	{
		string filename = _filename + ".xml";
		XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "да"));
		var str = XmlNodeWrite(node);
		doc.Add(str);
		doc.Save(filename);
		return File.Exists(filename);
	}
	//TODO: Доделать валидацию входного параметра.
	private XElement XmlNodeWrite(AbstractNode _node)
	{
		var newnode = new XElement(_node.tagName);
		switch (_node.GetType().Name) {
			case "Node":
				if (_node.Type != null)
					newnode.Add(new XAttribute("type",_node.Type));
				if (_node.Name != null)
					newnode.Add(new XAttribute("name",_node.Name));
				((Node)_node).Nodes.ForEach(nd => newnode.Add(XmlNodeWrite(nd)));
				break;
			case "PropertyNode":
				var propnode = (PropertyNode)_node;
				if (propnode.Type != null)
					newnode.Add(new XAttribute("type",propnode.Type));
				if (propnode.Name != null)
					newnode.Add(new XAttribute("name",propnode.Name));
				if (propnode.Text != null)
					newnode.Add(propnode.Text);
				break;
			case "ParamValuesNode":
				var parnode = (ParamValuesNode)_node;
				if (parnode.Code != null)
					newnode.Add(new XAttribute("code",parnode.Code));
				break;
 		}
		return newnode;
	}
	#endregion	 
	public static implicit operator Node(NodeBilder nodebilder){		
		return nodebilder.node;	}		
	}
}
