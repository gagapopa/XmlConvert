﻿/*
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
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			AddTableBtn.Visible = false;
			autoTable.Checked = true;
		}
		 
		public event EventHandler FileOpenClick;
		
		public event EventHandler FileSaveClick;			
		
		public event EventHandler TablesArrayListCommitted;
		
		public event EventHandler NextClick;
		
		public event EventHandler AddTableClick;
		
		public event EventHandler StationNameTextChange;
		
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

		public string OpenFileName {
			get;
			set;
		}
		
		public string CloseFileName {
			get;
			set;
		}
		
		public int TableNumberForView{
			get;
			set;
		}
		
		public string TableName {get {return TableNameTextBox.Text;}}
		
		public string StationName {get;private set;}

		public List<int> ListTmpl {
			get {
				return listTmpl;
			}
			set {
				listTmpl = value;
			}
		}
		
		public List<int> ListCol {
			get {
				return listCol;
			}
			set {
				listCol = value;
			}
		}
		public KindOfTable Knd {
			get;
			set;
		}

		private bool selectionColumnOn = false;
		
		private List<int> listCol = new List<int>();
		
		private List<int> listTmpl = new List<int>();	
		
		
		void OpnButtonClick(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "Файлы Word|*docx";
			if (dlg.ShowDialog() == DialogResult.OK) {
				OpenFileName = dlg.FileName;
				StationNameTextBox.Visible = true;
				StationNameTextBox.Enabled = true;
				if (FileOpenClick != null) FileOpenClick(this,EventArgs.Empty);
				upperLabel.Text = @"Введите название станции ->";
			}			
		}
		void SaveBtnClick(object sender, EventArgs e)
		{
			var dlg = new SaveFileDialog();			
			if (dlg.ShowDialog() == DialogResult.OK) {
				CloseFileName = dlg.FileName;
				if (FileSaveClick != null) FileSaveClick(this,EventArgs.Empty);
				AddTableBtn.Visible = false;
				AddTableBtn.Enabled = false;
				autoTable.Visible  = false;
				manualTable.Visible = false;
				TableNameTextBox.Visible = false;
			}		
	
		}
		void TablesArrayListSelectionChangeCommittedAsync(object sender, EventArgs e)
		{
			SaveBtn.Enabled = false;
			AddTableBtn.Visible = false;
			AddTableBtn.Enabled = false;
			TableNameTextBox.Visible = false;
			autoTable.Visible = false;
			manualTable.Visible = false;
			TableNumberForView = (int)TablesArrayList.SelectedItem;
			if (TablesArrayListCommitted != null)
				 TablesArrayListCommitted(this, EventArgs.Empty);			
			listCol.RemoveAll(x => true);
			listTmpl.RemoveAll(x => true);
			upperLabel.Text = "";
			infoLabel.Text = @"Выделите двойным кликом заголовок столбца с п/п";
			selectionColumnOn = true;
		}
		
		void NextBtnClick(object sender, EventArgs e)
		{
			if (!selectionColumnOn){
				if (NextClick != null) NextClick(this,EventArgs.Empty);
				AddTableBtn.Visible = true;
				AddTableBtn.Enabled = true;
				autoTable.Visible  = true;
				manualTable.Visible = true;
				TableNameTextBox.Visible = true;
				NextBtn.Enabled = false;
				infoLabel.Text = @"Проверте значения в таблице еще раз, введите название таблицы и нажмите AddTable";
			} else{
				selectionColumnOn = false;
				infoLabel.Text = @"Проверте значения в таблице и нажмите Next>>";
			}
		}
		
		void AddTableBtnClick(object sender, EventArgs e)
		{
			if (TableNameTextBox.Text.Length > 1) {
				if (autoTable.Checked) Knd = KindOfTable.TEP;
				if (manualTable.Checked) Knd = KindOfTable.manual;
				if (AddTableClick != null) AddTableClick(this, EventArgs.Empty);
				SaveBtn.Enabled = true;	
				infoLabel.Text = "Можете сохранить xml, нажав SaveFile";
			}
			else				
				MessageBox.Show("Введите название таблицы");
		}
		
		void StationNameTextBoxTextChanged(object sender, EventArgs e)
		{
			TablesArrayList.Visible |= StationNameTextBox.Text.Length > 0;
			infoLabel.Text = @"Выберите в ниcпадающем меню номер нужной вам таблицы";			
		}
		
		void DataTableViewColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (selectionColumnOn) {							
				if (listCol.Count <=4) {
					switch (listCol.Count) {
						case 0:
							listCol.Add(e.ColumnIndex);
							infoLabel.Text = @"Выделите двойным кликом заголовок столбца с Наименованием";
							break;
						case 1:							
							listCol.Add(e.ColumnIndex);
							infoLabel.Text = @"Выделите двойным кликом заголовок столбца с Размерностью";
							break;
						case 2:
							infoLabel.Text = @"Выделите двойным кликом заголовок столбца с Обозначением";							
							listCol.Add(e.ColumnIndex);
							break;
						case 3:
							infoLabel.Text = @"Выделите двойным кликом заголовок столбца с Формулой";
							SintaxLight(e.ColumnIndex);
							listCol.Add(e.ColumnIndex);
							break;
						case 4:												
							listCol.Add(e.ColumnIndex);
							infoLabel.Text = @"Выделите так же столбцы с шаблонами. Когда закончите, нажмите Next>>. Выделены: ";		
							break;	
					}
				} else {
					listTmpl.Add(e.ColumnIndex);
					infoLabel.Text += listTmpl.FindLast(x => true) + " ";
					NextBtn.Enabled |= listTmpl.Count > 0;
				}
			    	
			}
		}
		private void SintaxLight(int columnNumber)
		{
			for (int i = 0, DataTableViewRowsCount = DataTableView.Rows.Count; i < DataTableViewRowsCount; i++) {
				DataGridViewRow row = DataTableView.Rows[i];
				//TODO: Подсветка синтаксиса 
			}
		}
		void StationNameTextBoxLeave(object sender, EventArgs e)
		{
			if (StationNameTextBox.Text.Length > 0) {				
				StationName = StationNameTextBox.Text;
				if (StationNameTextChange != null) StationNameTextChange(this, EventArgs.Empty);
				StationNameTextBox.Enabled = false;
			}
	
		}	
	}	
		
	public interface IConvertForm
	{
		event EventHandler FileOpenClick;
		
		event EventHandler FileSaveClick;		
		
		event EventHandler TablesArrayListCommitted;
		
		event EventHandler NextClick;
		
		event EventHandler AddTableClick;
		
		event EventHandler StationNameTextChange;
		
		DataGridView DataTableForView {get;set;}
		
		List<int> ListTmpl {get;set;}
		
		List<int> ListCol {get;set;}
		
		int[] TablesForView {set;}
		
		int TableNumberForView {get;}
		
		string OpenFileName {get;set;}
		
		string CloseFileName {get;set;}
		
		string TableName {get;}
		
		string StationName {get;}
		
		KindOfTable Knd  {get;set;}
	}
}
