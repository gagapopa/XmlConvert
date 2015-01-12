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
using XmlConvertForIstok.Readers;
using System.Windows.Forms;

namespace XmlConvertForIstok
{
	
	/// <summary>
	/// Description of XMLConvertForm.
	/// </summary>
	public partial class XMLConvertForm : Form, IConvertForm
	{
		public XMLConvertForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			DataTableForView = DataTableView;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		 
		public event EventHandler FileOpenClick;
		
		public event EventHandler FileSaveClick;
		
		public event EventHandler SelectTableForView;		
		
		public DataGridView DataTableForView{
			get{
				if (DataTableView != null)
					return DataTableView;;
				return null;
			}
			set{
				DataTableView = value;
			}
		}		
		public int[] TablesForView{			
			set{
				TablesArrayList.DataSource = value;
			}
		}
		
		public string FileName{
			get;
			set;
		}
		
		void OpnButtonClick(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "Файлы Word|*docx";
			if (dlg.ShowDialog() == DialogResult.OK) {
				FileName = dlg.FileName;
				if (FileOpenClick != null) FileOpenClick(this,EventArgs.Empty);
			}			
		}
		void SaveBtnClick(object sender, EventArgs e)
		{
			var dlg = new SaveFileDialog();			
			if (dlg.ShowDialog() == DialogResult.OK) {
				FileName = dlg.FileName;
				if (FileSaveClick != null) FileSaveClick(this,EventArgs.Empty);
			}		
	
		}
	}
	
	public interface IConvertForm
	{
		event EventHandler FileOpenClick;
		
		event EventHandler FileSaveClick;
		
		event EventHandler SelectTableForView;
		
		DataGridView DataTableForView {get;set;}
		
		int[] TablesForView {set;}
		
		string FileName {get;set;}
	}
}
