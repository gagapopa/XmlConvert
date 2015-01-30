/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 29.01.2015
 * Время: 17:11
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;


namespace XmlConvertForIstok.Readers
{
	/// <summary>
	/// Description of ExcelReader.
	/// </summary>
	public class ExcelReader : WrdReader,IReader
	{		
		#region IReader implementation
		public new event ForProgress GetTableArrayProgress =  NullMethod;

		public new Task<int[]> GetTableArray(string _filename)
		{
			return Task.Run(() =>{
			            GetTableArrayProgress(0,100);
			         	var wrkBook = new XLWorkbook(_filename);
			         	int wrkSheetsCount = wrkBook.Worksheets.Count;
						var lstArray = new List<int>();
						for (int i = 0; i < wrkSheetsCount; i++) {
							lstArray.Add(i);
							GetTableArrayProgress(i,wrkSheetsCount);
						}
						wrkBook.Dispose();
						return lstArray.ToArray();
			         });
		}

		public new Task<DataTable> GetTable(int tbl, string _filename)
		{
			return Task.Run(() =>{
			            GetTableArrayProgress(0,100);
			         	var wrkBook = new XLWorkbook(_filename);
			         	var table = new DataTable();
			         	var wrkSheet = wrkBook.Worksheets.ToArray()[tbl];
			         	IXLTable xlTable =wrkSheet.Range(wrkSheet.Cell(1,1),wrkSheet.LastCellUsed()).AsTable();
			         	foreach (IXLTableField col in xlTable.Fields) {
			         		table.Columns.Add(col.Name);			         		
			         	}
			         	int rowsCount = xlTable.DataRange.RowCount();
			         	xlTable.DataRange.Rows(c => true)
			         		.ForEach(xrow => {			         		         	
			                         	var row = table.NewRow();
			                         	int cellsCount = xrow.Cells().Count();			         	                                 	
			                         	for (int i = 1; i <= cellsCount; i++) {			         	                                 		
			                         		row[i-1] = xrow.Cell(i).Value;
			                         	}
			                         	table.Rows.Add(row);
			                         	GetTableArrayProgress(xrow.RowNumber(),rowsCount);
			         		         });
			         	
						wrkBook.Dispose();
						return table;
			         });
		}
		#endregion
	}
}
