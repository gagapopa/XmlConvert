/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 10.01.2015
 * Время: 21:54
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using NUnit.Framework;
using XmlConvertForIstok.Readers;

namespace SecondTest
{
	[TestFixture]
	public class WrdReaderTest
	{
		[Test]
		public void OpenDocTest()
		{
			IReader rd = new WrdReader();
			
			int[] assert = rd.GetTableArray("TestWord.docx");
			
			Assert.IsTrue(assert[1] == 1);
		}
	}
}
