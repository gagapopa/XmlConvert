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
		IConvertForm form;
		
		IReader reader;
		
		ITableToModel tableToModel;
		
		public MyPresenter(IConvertForm _form,IReader _reader,ITableToModel _tableToModel)
		{
//			form.DataTableForView.DataSource = reader.GetTable(0, @"C:\Users\Господин\Documents\Mono\XmlConvertForIstok2\SecondTest\bin\Debug\TestWord.docx");
			form = _form;
			reader = _reader;
			tableToModel = _tableToModel;
			form.FileOpenClick += FileOpen;
		}
		
		void FileOpen(object sender, EventArgs e)
		{			
			form.DataTableForView.DataSource = reader.GetTable(0,form.FileName);
		}
		
	}
}
