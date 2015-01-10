/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 06.01.2015
 * Время: 15:22
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlConvertForIstok.Composite
{
	/// <summary>
	/// Класс древовидной структуры.
	/// </summary>
	/// 
	[Serializable]
	[XmlInclude(typeof(PropertyNode)),XmlInclude(typeof(ParamValuesNode)),XmlInclude(typeof(Node))]
	public abstract class AbstractNode
	{
		[XmlElement]
		public string tagName {
			get;
			set;
		}
		[XmlAttribute("name")]
		public string Name {
			get;
			set;
		}
		[XmlAttribute("type")]
		public string Type {
			get;
			set;
		}		
	}	
	
	public class Node : AbstractNode
	{	
		[XmlEnum]
		public List<AbstractNode> Nodes = new List<AbstractNode>();
		
	}
	
	public class PropertyNode : AbstractNode
	{
		[XmlAttribute]
		public string Text {
			get;
			set;
		}
		
	}
	
	public class ParamValuesNode : AbstractNode
	{
		[XmlAttribute]
		public string Code {
			get;
			set;
		}		
	}
}
