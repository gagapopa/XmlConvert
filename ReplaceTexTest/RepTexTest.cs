/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 24.12.2014ccc
 * Время: 14:38
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using NUnit.Framework;
using XmlConvertForIstok.Convert;

namespace ReplaceTexTest
{
	[TestFixture]
	public class RepTexTest
	{	
		[Test]
		public void EnterSimbol()
		{
			const string Str = @"q_4^исх+〖Δq〗_4^исх (А^р )+〖Δq〗_4^исх (W^р);";			
			
			string asserttxt = ReplaceTex.EnterSimbol(Str);
			
			Console.WriteLine(asserttxt);			
			StringAssert.Contains(@"$q_4\^исх$+$\Delta q_4\^исх (А\^р )$+$\Delta q_4\^исх (W\^р)$;", asserttxt);			
		}
		
		[Test]
		public void ReplaceText()
		{
			const string Str = @"(t_(ух,(уг)i)^н∙Q ̅_к(уг)i^бр+t_(ух,(г)i)^н∙Q ̅_к(г)i^бр)/Q ̅_кi^бр;";
			
			string enterstr = ReplaceTex.EnterSimbol(Str);
			string asserttxt = ReplaceTex.CleanedTex(enterstr);
			
			Console.WriteLine(asserttxt);			
			StringAssert.Contains(				
				@"($t\^н_{ух,(уг)i}$*$\bar Q^бр_{к(уг)i}$+$t\^н_{ух,(г)i}$*$\bar Q^бр)_{к(г)i}$/$\bar Q^бр_{кi}$;",
				asserttxt);			
		}
	}
}
