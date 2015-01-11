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
	public abstract class AbstractNode
	{
		public string tagName {
			get;
			set;
		}		
		public string Name {
			get;
			set;
		}		
		public string Type {
			get;
			set;
		}		
	}		
	public class Node : AbstractNode
	{		
		public List<AbstractNode> Nodes = new List<AbstractNode>();
	}	
	public class PropertyNode : AbstractNode
	{
		public string Text {
			get;
			set;
		}		
	}	
	public class ParamValuesNode : AbstractNode
	{
		public string Code {
			get;
			set;
		}		
	}
}
