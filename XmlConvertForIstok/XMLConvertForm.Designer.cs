/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 12.01.2015
 * Время: 13:48
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace XmlConvertForIstok
{
	partial class XMLConvertForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button OpnButton;
		private System.Windows.Forms.DataGridView DataTableView;
		private System.Windows.Forms.ComboBox TablesArrayList;
		private System.Windows.Forms.Button SaveBtn;
		private System.Windows.Forms.Button NextBtn;
		private System.Windows.Forms.TextBox TableNameTextBox;
		private System.Windows.Forms.Button AddTableBtn;
		private System.Windows.Forms.TextBox StationNameTextBox;
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.Label upperLabel;
		private System.Windows.Forms.RadioButton autoTable;
		private System.Windows.Forms.RadioButton manualTable;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.ComboBox intervalArray;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.OpnButton = new System.Windows.Forms.Button();
			this.DataTableView = new System.Windows.Forms.DataGridView();
			this.TablesArrayList = new System.Windows.Forms.ComboBox();
			this.SaveBtn = new System.Windows.Forms.Button();
			this.NextBtn = new System.Windows.Forms.Button();
			this.TableNameTextBox = new System.Windows.Forms.TextBox();
			this.AddTableBtn = new System.Windows.Forms.Button();
			this.StationNameTextBox = new System.Windows.Forms.TextBox();
			this.infoLabel = new System.Windows.Forms.Label();
			this.upperLabel = new System.Windows.Forms.Label();
			this.autoTable = new System.Windows.Forms.RadioButton();
			this.manualTable = new System.Windows.Forms.RadioButton();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.intervalArray = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.DataTableView)).BeginInit();
			this.SuspendLayout();
			// 
			// OpnButton
			// 
			this.OpnButton.Location = new System.Drawing.Point(12, 12);
			this.OpnButton.Name = "OpnButton";
			this.OpnButton.Size = new System.Drawing.Size(75, 23);
			this.OpnButton.TabIndex = 1;
			this.OpnButton.Text = "OpenFile";
			this.OpnButton.UseVisualStyleBackColor = true;
			this.OpnButton.Click += new System.EventHandler(this.OpnButtonClick);
			// 
			// DataTableView
			// 
			this.DataTableView.AllowUserToOrderColumns = true;
			this.DataTableView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.DataTableView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.DataTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataTableView.ImeMode = System.Windows.Forms.ImeMode.On;
			this.DataTableView.Location = new System.Drawing.Point(12, 41);
			this.DataTableView.Name = "DataTableView";
			this.DataTableView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DataTableView.Size = new System.Drawing.Size(990, 454);
			this.DataTableView.TabIndex = 2;
			this.DataTableView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataTableViewCellMouseDoubleClick);
			this.DataTableView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataTableViewColumnHeaderMouseDoubleClick);
			// 
			// TablesArrayList
			// 
			this.TablesArrayList.FormattingEnabled = true;
			this.TablesArrayList.Location = new System.Drawing.Point(12, 501);
			this.TablesArrayList.Name = "TablesArrayList";
			this.TablesArrayList.Size = new System.Drawing.Size(165, 21);
			this.TablesArrayList.TabIndex = 3;
			this.TablesArrayList.Visible = false;
			this.TablesArrayList.SelectionChangeCommitted += new System.EventHandler(this.TablesArrayListSelectionChangeCommittedAsync);
			// 
			// SaveBtn
			// 
			this.SaveBtn.Enabled = false;
			this.SaveBtn.Location = new System.Drawing.Point(927, 502);
			this.SaveBtn.Name = "SaveBtn";
			this.SaveBtn.Size = new System.Drawing.Size(75, 23);
			this.SaveBtn.TabIndex = 4;
			this.SaveBtn.Text = "SaveFile";
			this.SaveBtn.UseVisualStyleBackColor = true;
			this.SaveBtn.Click += new System.EventHandler(this.SaveBtnClick);
			// 
			// NextBtn
			// 
			this.NextBtn.Enabled = false;
			this.NextBtn.Location = new System.Drawing.Point(657, 499);
			this.NextBtn.Name = "NextBtn";
			this.NextBtn.Size = new System.Drawing.Size(75, 23);
			this.NextBtn.TabIndex = 5;
			this.NextBtn.Text = "Next >>";
			this.NextBtn.UseVisualStyleBackColor = true;
			this.NextBtn.Click += new System.EventHandler(this.NextBtnClick);
			// 
			// TableNameTextBox
			// 
			this.TableNameTextBox.Location = new System.Drawing.Point(183, 502);
			this.TableNameTextBox.Name = "TableNameTextBox";
			this.TableNameTextBox.Size = new System.Drawing.Size(468, 20);
			this.TableNameTextBox.TabIndex = 6;
			this.TableNameTextBox.Visible = false;
			// 
			// AddTableBtn
			// 
			this.AddTableBtn.Enabled = false;
			this.AddTableBtn.Location = new System.Drawing.Point(576, 528);
			this.AddTableBtn.Name = "AddTableBtn";
			this.AddTableBtn.Size = new System.Drawing.Size(75, 23);
			this.AddTableBtn.TabIndex = 7;
			this.AddTableBtn.Text = "AddTable";
			this.AddTableBtn.UseVisualStyleBackColor = true;
			this.AddTableBtn.Click += new System.EventHandler(this.AddTableBtnClick);
			// 
			// StationNameTextBox
			// 
			this.StationNameTextBox.Location = new System.Drawing.Point(611, 15);
			this.StationNameTextBox.Name = "StationNameTextBox";
			this.StationNameTextBox.Size = new System.Drawing.Size(391, 20);
			this.StationNameTextBox.TabIndex = 8;
			this.StationNameTextBox.Visible = false;
			this.StationNameTextBox.TextChanged += new System.EventHandler(this.StationNameTextBoxTextChanged);
			this.StationNameTextBox.Leave += new System.EventHandler(this.StationNameTextBoxLeave);
			// 
			// infoLabel
			// 
			this.infoLabel.Location = new System.Drawing.Point(12, 555);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(990, 23);
			this.infoLabel.TabIndex = 9;
			// 
			// upperLabel
			// 
			this.upperLabel.Location = new System.Drawing.Point(157, 12);
			this.upperLabel.Name = "upperLabel";
			this.upperLabel.Size = new System.Drawing.Size(410, 23);
			this.upperLabel.TabIndex = 10;
			// 
			// autoTable
			// 
			this.autoTable.Location = new System.Drawing.Point(183, 527);
			this.autoTable.Name = "autoTable";
			this.autoTable.Size = new System.Drawing.Size(104, 24);
			this.autoTable.TabIndex = 11;
			this.autoTable.TabStop = true;
			this.autoTable.Text = "АвтоВвод";
			this.autoTable.UseVisualStyleBackColor = true;
			this.autoTable.Visible = false;
			// 
			// manualTable
			// 
			this.manualTable.Location = new System.Drawing.Point(317, 528);
			this.manualTable.Name = "manualTable";
			this.manualTable.Size = new System.Drawing.Size(104, 24);
			this.manualTable.TabIndex = 12;
			this.manualTable.TabStop = true;
			this.manualTable.Text = "РучнойВвод";
			this.manualTable.UseVisualStyleBackColor = true;
			this.manualTable.Visible = false;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(13, 598);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(164, 23);
			this.progressBar.TabIndex = 13;
			// 
			// intervalArray
			// 
			this.intervalArray.FormattingEnabled = true;
			this.intervalArray.Items.AddRange(new object[] {
			"час",
			"вахта",
			"сут",
			"месяц"});
			this.intervalArray.Location = new System.Drawing.Point(739, 499);
			this.intervalArray.Name = "intervalArray";
			this.intervalArray.Size = new System.Drawing.Size(121, 21);
			this.intervalArray.TabIndex = 14;
			this.intervalArray.Visible = false;
			// 
			// XMLConvertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1014, 617);
			this.Controls.Add(this.intervalArray);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.manualTable);
			this.Controls.Add(this.autoTable);
			this.Controls.Add(this.upperLabel);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.StationNameTextBox);
			this.Controls.Add(this.AddTableBtn);
			this.Controls.Add(this.TableNameTextBox);
			this.Controls.Add(this.NextBtn);
			this.Controls.Add(this.SaveBtn);
			this.Controls.Add(this.TablesArrayList);
			this.Controls.Add(this.DataTableView);
			this.Controls.Add(this.OpnButton);
			this.Name = "XMLConvertForm";
			this.Text = "XMLConvertForm";
			((System.ComponentModel.ISupportInitialize)(this.DataTableView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
