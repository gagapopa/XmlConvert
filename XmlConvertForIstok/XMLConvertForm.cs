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
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XmlConvertForIstok
{
	/// <summary>
	/// Description of XMLConvertForm.
	/// </summary>
	public partial class XMLConvertForm : Form, IConvertForm, IFormEventsForExcel
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
			Ptog += ProgressBar;
			
		}

		public ForProgress Ptog {
			get;
			set;
		}
		
		public event EventHandler FileOpenClick = delegate{ };
		
		public event EventHandler FileExcelOpenClick = delegate{ };
		
		public event EventHandler SaveAsExcelClick = delegate{ };
		
		public event EventHandler FileSaveClick  = delegate{ };			
		
		public event EventHandler TablesArrayListCommitted  = delegate{ };
		
		public event EventHandler TablesArrayListCommittedExcel  = delegate{ };
		
		public event EventHandler NextClick  = delegate{ };
		
		public event EventHandler AddTableClick = delegate{ };
		
		public event EventHandler StationNameTextChange = delegate{ };
		
		public DataTable DataTableForView{
			get{
				if (DataTableView != null)
					return DataTableView.DataSource as DataTable;;
				return null;
			}
			set{			
				DataTableView.DataSource = value;
				foreach (DataGridViewColumn  col in DataTableView.Columns)
					col.SortMode = DataGridViewColumnSortMode.NotSortable;
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
		
		public string IntervalStr {get;private set;}

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
		
		private bool editingCellsOn;
		
		private OpenFileType fileType;
		
		void OpnButtonClick(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "Файлы Word|*.docx|Файлы Excel|*.xlsx";
			if (dlg.ShowDialog() == DialogResult.OK) {
				OpenFileName = dlg.FileName;
				StationNameTextBox.Visible = true;
				StationNameTextBox.Enabled = true;
				switch (dlg.FilterIndex) {
					case 1:
						FileOpenClick(this, EventArgs.Empty);
						fileType = OpenFileType.Word;
						break;	
					case 2:
						FileExcelOpenClick(this, EventArgs.Empty);
						fileType = OpenFileType.Excel;
						break;
				}
				upperLabel.Text = @"Введите название станции ->";
				OpnButton.Enabled = false;
			}			
		}
		void SaveBtnClick(object sender, EventArgs e)
		{
			var dlg = new SaveFileDialog();			
			if (dlg.ShowDialog() == DialogResult.OK) {
				CloseFileName = dlg.FileName;
				FileSaveClick(this,EventArgs.Empty);
				AddTableBtn.Visible = false;
				AddTableBtn.Enabled = false;
				autoTable.Visible  = false;
				manualTable.Visible = false;
				TableNameTextBox.Visible = false;
				editingCellsOn = false;
				OpnButton.Enabled = true;
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
			intervalArray.Visible = false;
			SaveAsExcelBtn.Enabled = true;
			TableNumberForView = (int)TablesArrayList.SelectedItem;
			if(fileType == OpenFileType.Word) TablesArrayListCommitted(this, EventArgs.Empty);
			if(fileType == OpenFileType.Excel) TablesArrayListCommittedExcel(this, EventArgs.Empty);
			listCol.RemoveAll(x => true);
			listTmpl.RemoveAll(x => true);
			upperLabel.Text = "";
			infoLabel.Text = @"Выделите двойным кликом заголовок столбца с п/п";
			selectionColumnOn = true;
		}
		
		void NextBtnClick(object sender, EventArgs e)
		{
			if (!selectionColumnOn){
				NextClick(this,EventArgs.Empty);
				AddTableBtn.Visible = true;
				AddTableBtn.Enabled = true;
				autoTable.Visible  = true;
				manualTable.Visible = true;
				TableNameTextBox.Visible = true;
				NextBtn.Enabled = false;
				intervalArray.Visible = true;
				infoLabel.Text = @"Проверте значения в таблице еще раз, введите название таблицы, интервал и нажмите AddTable";
			} else{
				editingCellsOn = true;
				selectionColumnOn = false;
				infoLabel.Text = @"Проверте значения в таблице и нажмите Next>>";
			}
		}
		
		void AddTableBtnClick(object sender, EventArgs e)
		{
			if (TableNameTextBox.Text.Length > 1 && intervalArray.Text.Length >1) {
				if (autoTable.Checked) Knd = KindOfTable.TEP;
				if (manualTable.Checked) Knd = KindOfTable.manual;
				IntervalStr = intervalArray.Text;
				AddTableClick(this, EventArgs.Empty);
				SaveBtn.Enabled = true;	
				infoLabel.Text = "Можете сохранить xml, нажав SaveFile";
			}
			else				
				MessageBox.Show("Введите название таблицы и выберите интервал");
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
		private  int ProgressBar(int now, int all)
		{
			Action act = () => {
	    	progressBar.Maximum = all;
	    	progressBar.Value = now +1;	    	
			};
			
			if (InvokeRequired) {
				Invoke(act);
			}else{
				act();
			}
	    	return 0;
		}
		void StationNameTextBoxLeave(object sender, EventArgs e)
		{
			if (StationNameTextBox.Text.Length > 0) {				
				StationName = StationNameTextBox.Text;
				StationNameTextChange(this, EventArgs.Empty);
				StationNameTextBox.Enabled = false;
			}
		}
		
		void DataTableViewCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (editingCellsOn && e.ColumnIndex >= 0) {
				Form txtForm = new TextForm(DataTableView.Rows[e.RowIndex].Cells[e.ColumnIndex]);
				txtForm.ShowDialog();
			}	
		}
		void ВставитьСтрокуаToolStripMenuItemClick(object sender, EventArgs e)
		{
			var cell = DataTableView.SelectedCells.Cast<DataGridViewCell>().First();
			var row = DataTableForView.NewRow();
			DataTableForView.Rows.InsertAt(row,cell.RowIndex);
		}
		void УдалитьСтрокуToolStripMenuItemClick(object sender, EventArgs e)
		{
			var cell = DataTableView.SelectedCells.Cast<DataGridViewCell>().First();			
			DataTableForView.Rows.Remove(DataTableForView.Rows[cell.RowIndex]);
		}
		void SaveAsExcelBtnClick(object sender, EventArgs e)
		{
			var dlg = new SaveFileDialog();			
			if (dlg.ShowDialog() == DialogResult.OK) {
				CloseFileName = dlg.FileName;
				SaveAsExcelClick(this,EventArgs.Empty);				
			}	
		}
	}
}
