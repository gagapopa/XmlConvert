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
		var xmlser = new XmlSerializer(node.GetType()); 
        var writer = new StreamWriter(filename);
        xmlser.Serialize(writer, node);
        writer.Flush();
        writer.Close();  
        return File.Exists(filename); 
		
	}

	#endregion
	
	public static implicit operator Node(NodeBilder nodebilder){		
		return nodebilder.node;	}
		
	}
}
