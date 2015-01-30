/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 29.01.2015
 * Время: 17:12
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using NUnit.Framework;
using System.Data;
using XmlConvertForIstok.Readers;

namespace SecondTest
{
	[TestFixture]
	public class ExcelReaderTest
	{
		[Test]
		public void GetTableArrayTest()
		{
			IReader reader = new ExcelReader();
			const string FileName = "ExcelForTesting.xlsx";
			int[] shtsArray = reader.GetTableArray(FileName).Result;
			
			Assert.IsTrue(shtsArray.Length == 3 && shtsArray[2] == 2);			
		}
		
		[Test]
		public void GetTableArrayProgressTest()
		{
			IReader reader = new ExcelReader();
			const string FileName = "ExcelForTesting.xlsx";
			int intAssert = 0;
			reader.GetTableArrayProgress += (n, a) => intAssert++;
			int[] shtsArray = reader.GetTableArray(FileName).Result;
			
			Assert.IsTrue(intAssert == 4);
		}
		
		[Test]
		public void GetTableTest()
		{
			IReader reader = new ExcelReader();
			const string FileName = "ExcelForTesting.xlsx";
			DataTable table = reader.GetTable(0, FileName).Result;
			string asserting = table.Rows[0][0].ToString();
			
			StringAssert.Contains("testtext21", asserting);
		}
		
		[Test]
		public void GetTableProgressTest()
		{
			IReader reader = new ExcelReader();
			const string FileName = "ExcelForTesting.xlsx";
			int intAssert = 0;
			reader.GetTableArrayProgress += (n, a) => intAssert++;
			DataTable table = reader.GetTable(0, FileName).Result;
			
			Assert.IsTrue(intAssert == 2);
		}
	}
}
