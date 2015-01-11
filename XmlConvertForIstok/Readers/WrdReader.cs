/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:53
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

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
				var tables = myDocument.MainDocumentPart.Document.Body.Elements<Table>();
				if (tables != null) {
					for (int i = 0; i < tables.Count(); i++) {
						tblarr.Add(i);
					}
				}
				return tblarr.ToArray();
			}
		}


		public DataTable GetTable(int tbl)
		{
			throw new NotImplementedException();
		}


		#endregion

		public void OpenDoc(string _filename)
		{
			using (WordprocessingDocument myDocument = WordprocessingDocument.Open(_filename, true)) {
				var gg = myDocument.MainDocumentPart.Document.Body.Elements<Table>().First();
				var rw = gg.Elements<TableRow>().ElementAt(1).Elements<TableCell>().ElementAt(0).InnerText;
				
				var pPr = rw.Length;
			}
		}
	}
}
