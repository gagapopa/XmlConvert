/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:52
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Threading.Tasks;
using System.Data;

namespace XmlConvertForIstok.Readers
{
	
	/// <summary>
	/// Description of IReader.
	/// </summary>
	public interface IReader
	{
		Task<int[]> GetTableArray(string _filename);
		
		Task<DataTable> GetTable(int tbl,string _filename);		

		DataTable TableCleanTex(DataTable tbl);
		
		event ForProgress GetTableArrayProgress;
	}
}
