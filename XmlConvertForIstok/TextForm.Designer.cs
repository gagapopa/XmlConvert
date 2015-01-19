/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 16.01.2015
 * Время: 18:34
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace XmlConvertForIstok
{
	partial class TextForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.Button CanselBtn;
		private System.Windows.Forms.Button OkBtn;
		
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
			this.richTextBox = new System.Windows.Forms.RichTextBox();
			this.CanselBtn = new System.Windows.Forms.Button();
			this.OkBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// richTextBox
			// 
			this.richTextBox.Location = new System.Drawing.Point(12, 12);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.Size = new System.Drawing.Size(727, 229);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			this.richTextBox.TextChanged += new System.EventHandler(this.RichTextBoxTextChanged);
			// 
			// CanselBtn
			// 
			this.CanselBtn.Location = new System.Drawing.Point(664, 247);
			this.CanselBtn.Name = "CanselBtn";
			this.CanselBtn.Size = new System.Drawing.Size(75, 23);
			this.CanselBtn.TabIndex = 1;
			this.CanselBtn.Text = "Cancel";
			this.CanselBtn.UseVisualStyleBackColor = true;
			this.CanselBtn.Click += new System.EventHandler(this.CanselBtnClick);
			// 
			// OkBtn
			// 
			this.OkBtn.Location = new System.Drawing.Point(583, 247);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.Size = new System.Drawing.Size(75, 23);
			this.OkBtn.TabIndex = 2;
			this.OkBtn.Text = "Ok";
			this.OkBtn.UseVisualStyleBackColor = true;
			this.OkBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// TextForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(751, 279);
			this.Controls.Add(this.OkBtn);
			this.Controls.Add(this.CanselBtn);
			this.Controls.Add(this.richTextBox);
			this.Name = "TextForm";
			this.Text = "TextForm";
			this.ResumeLayout(false);

		}
	}
}
