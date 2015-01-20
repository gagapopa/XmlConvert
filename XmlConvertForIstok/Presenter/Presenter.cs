/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 01/12/2015
 * Время: 17:52
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
	/// Description of Presenter.
	/// </summary>
	public class MyPresenter
	{
		IConvertForm form;
		
		IReader reader;
		
		ITableToModel tableToModel;
		
		public MyPresenter(IConvertForm _form,IReader _reader,ITableToModel _tableToModel)
		{
			form = _form;
			reader = _reader;
			tableToModel = _tableToModel;			
		
			reader.GetTableArrayProgress += form.Ptog;
			
			form.FileOpenClick += FileOpen;
			form.StationNameTextChange += StationNameChanged;
			form.TablesArrayListCommitted += ShowTable;
			form.NextClick += NextClickTableShow;
			form.AddTableClick += AddTableClick;
			form.FileSaveClick += SaveFile;
		}
		
		public async void FileOpen(object sender,  EventArgs e)
		{
			form.TablesForView = await reader.GetTableArray(form.OpenFileName);
		}
		
		public void StationNameChanged (object sender,  EventArgs e)
		{
			tableToModel.CreateStation(form.StationName);
		}
		
		public async void ShowTable(object sender,  EventArgs e)
		{			
			form.DataTableForView = await reader.GetTable(form.TableNumberForView, form.OpenFileName);
//			foreach (DataGridViewColumn  col in form.DataTableForView.Columns) {
//				col.SortMode = DataGridViewColumnSortMode.NotSortable;
//			}
		}
		
		public void NextClickTableShow(object sender,  EventArgs e)
		{
			DataTable tbl;
			tbl = form.DataTableForView;
			form.DataTableForView = reader.TableCleanTex(tbl, form.ListCol);		
		}
		
		public void AddTableClick(object sender,  EventArgs e)
		{
			tableToModel.TableToModelConv(form.TableName,
			                              form.DataTableForView,
			                              form.ListCol,
			                              form.ListTmpl,
			                              form.IntervalStr,
			                              form.Knd);
		}
		
		public void SaveFile(object sender,  EventArgs e)
		{
			tableToModel.SerializeModel(form.CloseFileName);
		}
		
	}
}
