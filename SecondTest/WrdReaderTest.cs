/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:54
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Data;
using NUnit.Framework;
using XmlConvertForIstok.Readers;

namespace SecondTest
{
	[TestFixture]
	public class WrdReaderTest
	{
		[Test]
		public void GetTableArrayTest()
		{
			IReader rd = new WrdReader();
			
			int[] assert = rd.GetTableArray("TestWord.docx");
			
			Assert.IsTrue(assert[1] == 1);
		}
		
		[Test]
		public void GetDataTableTest()
		{
			IReader rd = new WrdReader();
			var tbl = rd.GetTable(1, "TestWord.docx");
			DataColumn coll = tbl.Columns[0];
			
			StringAssert.Contains("asd", coll.ColumnName);
		}
		
		[Test]
		public void GetDataTable()
		{
			IReader rd = new WrdReader();
			DataTable tbl = rd.GetTable(1, "TestWord.docx");
			DataRow row = tbl.Rows[0];
			
			StringAssert.Contains("qqq", row[0].ToString());
		}
	}
}
