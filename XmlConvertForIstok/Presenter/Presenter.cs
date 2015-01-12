/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 01/12/2015
 * Время: 17:52
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using XmlConvertForIstok.Readers;
using XmlConvertForIstok.Composite;

namespace XmlConvertForIstok.Presenter
{
	/// <summary>
	/// Description of Presenter.
	/// </summary>
	public class MyPresenter
	{
		public MyPresenter(IConvertForm form,IReader reader,INodeBilder nodeBilder)
		{
			form.DataTableForView.DataSource = reader.GetTable(0, @"C:\Users\Господин\Documents\Mono\XmlConvertForIstok2\SecondTest\bin\Debug\TestWord.docx");
		}
		
	}
}
