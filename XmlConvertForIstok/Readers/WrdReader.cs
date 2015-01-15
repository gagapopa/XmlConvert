/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:53
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using XmlConvertForIstok.Convert;

namespace XmlConvertForIstok.Readers
{
	/// <summary>
	/// Description of WrdReader.
	/// </summary>
	public class WrdReader :IReader
	{
		#region IReader implementation
		
		public int[] GetTableArray(string _filename)
		{
			var tblarr = new List<int>();
			using (WordprocessingDocument myDocument = WordprocessingDocument.Open(_filename, true)) {
				var tables = myDocument.MainDocumentPart.Document.Body
					.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>();
				if (tables != null) {
					for (int i = 0; i < tables.Count(); i++) {						
						tblarr.Add(i);
					}
				}				
			}
			return tblarr.ToArray();
		}


		public DataTable GetTable(int tbl,string _filename)
		{
			var dataTableForReturn = new DataTable();
			using (WordprocessingDocument myDocument = WordprocessingDocument.Open(_filename, false)) {
				DocumentFormat.OpenXml.Wordprocessing.Table tabl = 
					myDocument.MainDocumentPart.Document.Body
					.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>().ElementAt(tbl);
				if (tabl != null) {
					int  rowNumber = tabl.Elements<TableRow>().Count();
					int coumnNumber = tabl.Elements<TableRow>().ElementAt(0).Elements<TableCell>().Count();
					
					for (int i = 0; i < rowNumber; i++) {
						DataRow nrow = null;
						if (i>0) {
							nrow = dataTableForReturn.NewRow();
						}						
						for (int j = 0; j < coumnNumber; j++) {
							string cell = tabl.Elements<TableRow>().ElementAt(i).Elements<TableCell>().ElementAt(j).InnerText.Trim(' ');
							if (i == 0) {
								dataTableForReturn.Columns.Add(cell);
								continue;
							}							
							if (nrow != null)
								nrow[j] = ReplaceTex.EnterSimbol(cell);
						}
						if (nrow != null)
							dataTableForReturn.Rows.Add(nrow);
					}
				}
			}
			return dataTableForReturn;
		}
		

		public DataTable TableCleanTex(DataTable tbl)
		{
			foreach (DataRow row in tbl.Rows) {
				for (int i = 0; i < tbl.Columns.Count; i++) {
					row[i] = ReplaceTex.CleanedTex(row[i].ToString());
				}
			}
			return tbl;
		}		
	#endregion
	}
}
