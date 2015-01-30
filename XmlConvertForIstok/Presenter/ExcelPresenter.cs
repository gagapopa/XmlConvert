/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 29.01.2015
 * Время: 12:26
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using XmlConvertForIstok.Readers;
using XmlConvertForIstok.Composite;

namespace XmlConvertForIstok.Presenter
{
	/// <summary>
	/// Description of ExcelPresenter.
	/// </summary>
	public class ExcelPresenter : MyPresenter
	{
		public ExcelPresenter(IConvertForm _form,IReader _reader,ITableToModel _tableToMode)
			:base(_form,_reader,_tableToMode)
		{
			
		}
	}
}
