/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 06.01.2015
 * Время: 21:14
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace XmlConvertForIstok.Composite
{
	/// <summary>
	/// Интерфейс для билдера модели
	/// </summary>
	public interface INodeBilder
	{
//	<summary>
//		добавление новой ноды.
//	</summary>
//	<returns>
//	Билдер новообразованной ноды
//	</returns>
		INodeBilder AddNode(string name,string type);
		
		INodeBilder AddProperty(string name, string text);		
		
		INodeBilder AddParamValues(string code);

		int GetNodesNumber();
		
		bool Serialyze(string filename);
	}
}
