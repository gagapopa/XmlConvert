/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:54
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
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
			
			int[] assert = rd.GetTableArray("TestWord.docx").Result;
			
			Assert.IsTrue(assert[1] == 1);
		}
		
		[Test]
		public void GetDataTableTest()
		{
			IReader rd = new WrdReader();
			var tbl = rd.GetTable(0, "TestWord.docx").Result;
			DataColumn coll = tbl.Columns[0];
			
			StringAssert.Contains("111", coll.ColumnName);
		}
		
		[Test]
		public void GetDataTable()
		{
			IReader rd = new WrdReader();
			DataTable tbl = rd.GetTable(1, "TestWord.docx").Result;
			DataRow row = tbl.Rows[0];
			
			StringAssert.Contains(@"$G_2$*550/$\Delta t$", row[1].ToString());
		}
		
		[Test]
		public void TableReplaceTexTest()
		{
			IReader rd = new WrdReader();
			DataTable tbl = rd.GetTable(1, "TestWord.docx").Result;
			var listint = new List<int> { 0, 1 };
			DataTable tblTex = rd.TableCleanTex(tbl, listint);

			DataRow row = tblTex.Rows[1];
			
			StringAssert.Contains(@"($ G^q  )_{tt}$/$\Delta t$", row[1].ToString());
		}
		
		[Test]
		public void NumberRowsTest()
		{
			IReader rd = new WrdReader();
			DataTable tbl = rd.GetTable(3, "TestWord.docx").Result;
			
			DataRow row = tbl.Rows[1];
			
			StringAssert.Contains(@"1.", row[0].ToString());
		}
	}
}
