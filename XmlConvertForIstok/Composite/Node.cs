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

namespace XmlConvertForIstok.Composite
{
	/// <summary>
	/// Класс древовидной структуры.
	/// </summary>
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
		private List<AbstractNode> nodes = new List<AbstractNode>();
		
		public List<AbstractNode> Nodes{
			get{return nodes;}
			set{nodes = value;}
		}
		
	}
	public class PropertyNode : AbstractNode
	{
		public string Text {
			get;
			set;
		}
		
	}
}
