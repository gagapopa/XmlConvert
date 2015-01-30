/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 29.01.2015
 * Время: 11:04
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.IO;
using NUnit.Framework;
using System.Data;
using Moq.Language;
using XmlConvertForIstok.Composite;

namespace SecondTest
{
	[TestFixture]
	public class TableToExcelTest
	{
		[Test]
		public void CreateStationTest()
		{
			ITableToModel exsaver = new TableToExcel();
			var forAssert = exsaver.CreateStation("TestStation");
			
			Assert.IsNotNull(forAssert as TableToExcel);
		}
		
		[Test]
		public void TableToModelConvTest()
		{
			ITableToModel exsaver = new TableToExcel();
			var table = new DataTable();
			
			table.Columns.Add("TestColumn");
			var row = table.NewRow();
			row[0] = "testtext";
			table.Rows.Add(row);
			exsaver.CreateStation("TestStation");
			bool asserting = exsaver.TableToModelConv("TestTable", table, null, null, null, KindOfTable.manual);
			
			Assert.IsTrue(asserting);
		}
		
		[Test]
		public void SerializeModelTest()
		{
			ITableToModel exsaver = new TableToExcel();
			var table = new DataTable();
			const string fileName = "ExcelSaverTest";
			if (File.Exists(fileName)) File.Delete(fileName);
			
			table.Columns.Add("TestColumn");
			var row = table.NewRow();
			row[0] = "testtext";
			table.Rows.Add(row);
			exsaver.CreateStation("TestStation");
			exsaver.TableToModelConv("TestTable", table, null, null, null, KindOfTable.manual);
			exsaver.SerializeModel(fileName);
			
			Assert.IsTrue(File.Exists(fileName + ".xlsx"));
		}
	}
}
