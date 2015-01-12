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
			this.DataTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DataTableView.Location = new System.Drawing.Point(12, 41);
			this.DataTableView.Name = "DataTableView";
			this.DataTableView.Size = new System.Drawing.Size(990, 454);
			this.DataTableView.TabIndex = 2;
			// 
			// TablesArrayList
			// 
			this.TablesArrayList.FormattingEnabled = true;
			this.TablesArrayList.Location = new System.Drawing.Point(12, 501);
			this.TablesArrayList.Name = "TablesArrayList";
			this.TablesArrayList.Size = new System.Drawing.Size(165, 21);
			this.TablesArrayList.TabIndex = 3;
			// 
			// SaveBtn
			// 
			this.SaveBtn.Location = new System.Drawing.Point(183, 501);
			this.SaveBtn.Name = "SaveBtn";
			this.SaveBtn.Size = new System.Drawing.Size(75, 23);
			this.SaveBtn.TabIndex = 4;
			this.SaveBtn.Text = "SaveFile";
			this.SaveBtn.UseVisualStyleBackColor = true;
			this.SaveBtn.Click += new System.EventHandler(this.SaveBtnClick);
			// 
			// XMLConvertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1014, 617);
			this.Controls.Add(this.SaveBtn);
			this.Controls.Add(this.TablesArrayList);
			this.Controls.Add(this.DataTableView);
			this.Controls.Add(this.OpnButton);
			this.Name = "XMLConvertForm";
			this.Text = "XMLConvertForm";
			((System.ComponentModel.ISupportInitialize)(this.DataTableView)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
