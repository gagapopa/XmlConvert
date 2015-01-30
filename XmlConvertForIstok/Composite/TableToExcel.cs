/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 29.01.2015
 * Время: 11:02
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace XmlConvertForIstok.Composite
{
	/// <summary>
	/// Сохранение таблицы в файл Эксель
	/// </summary>
	public class TableToExcel : ITableToModel
	{
		private string fileName;
		private XLWorkbook wrkBook;
		
		#region ITableToModel implementation

		public ITableToModel CreateStation(string stationName)
		{
			fileName = stationName;
			wrkBook = new XLWorkbook();
			return this;
		}

		public bool TableToModelConv(string tblName, DataTable table, List<int> listColumnsNumber, List<int> listtmplNumber, string interval, KindOfTable kot)
		{			
			var wrkSheet = wrkBook.Worksheets.Add(tblName);
			wrkSheet.Cell(1,1).InsertTable(table);
			return true;
		}

		public void SerializeModel(string fileName)
		{
			wrkBook.SaveAs(fileName + ".xlsx");			
		}

		#endregion
	}
}
