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
	/// Паралельно работающий презентер для сохранения модели в Эксель
	/// </summary>
	public class ExcelPresenter : MyPresenter
	{
		private IFormEventsForExcel excelEvent;
		
		public ExcelPresenter(IConvertForm _form,IReader _reader,ITableToModel _tableToMode, IFormEventsForExcel _excelEvent)
			:base(_form,_reader,_tableToMode)
		{
			excelEvent = _excelEvent;
			
			form.FileOpenClick -= FileOpen;
			form.AddTableClick -= AddTableClick;
			form.FileSaveClick -= SaveFile;
			form.TablesArrayListCommitted -= ShowTable;
			
			excelEvent.FileExcelOpenClick += FileOpen;
			excelEvent.SaveAsExcelClick += AddTableClick;
			excelEvent.SaveAsExcelClick += SaveFile;
			excelEvent.TablesArrayListCommittedExcel += ShowTable;
		}
		
	}
}
