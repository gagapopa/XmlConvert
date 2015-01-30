/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 12.01.2015
 * Время: 13:48
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Data;
using System.ComponentModel;
using XmlConvertForIstok.Readers;
using System.Threading.Tasks;
using XmlConvertForIstok.Composite;
using System.Collections.Generic;
using System.Windows.Forms;
namespace XmlConvertForIstok
{
	public interface IConvertForm
	{
		ForProgress Ptog {
			get;
			set;
		}
		
		event EventHandler FileOpenClick;

		event EventHandler FileSaveClick;

		event EventHandler TablesArrayListCommitted;

		event EventHandler NextClick;

		event EventHandler AddTableClick;

		event EventHandler StationNameTextChange;

		DataTable DataTableForView {
			get;
			set;
		}

		List<int> ListTmpl {
			get;
			set;
		}

		List<int> ListCol {
			get;
			set;
		}

		int[] TablesForView {
			set;
		}

		int TableNumberForView {
			get;
		}

		string OpenFileName {
			get;
			set;
		}

		string CloseFileName {
			get;
			set;
		}

		string TableName {
			get;
		}

		string StationName {
			get;
		}

		string IntervalStr {
			get;
		}

		KindOfTable Knd {
			get;
			set;
		}
	}
}


