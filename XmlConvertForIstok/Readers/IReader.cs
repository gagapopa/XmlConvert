/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:52
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;

namespace XmlConvertForIstok.Readers
{
	/// <summary>
	/// Description of IReader.
	/// </summary>
	public interface IReader
	{
		int[] GetTableArray(string _filename);
		
		DataTable GetTable(int tbl);		
	}
}
