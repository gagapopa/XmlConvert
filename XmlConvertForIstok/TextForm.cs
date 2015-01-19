/*
 * Создано в SharpDevelop.
 * Пользователь: Господин
 * Дата: 16.01.2015
 * Время: 18:34
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace XmlConvertForIstok
{
	/// <summary>
	/// Description of TextForm.
	/// </summary>
	public partial class TextForm : Form
	{		
		private DataGridViewCell cell;
		public TextForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent(); 
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public TextForm(DataGridViewCell _cell)
			:this()
		{
			cell = _cell;
			richTextBox.Text = cell.Value.ToString();
			RichTextBoxTextChanged(this,EventArgs.Empty);
		}
		void RichTextBoxTextChanged(object sender, EventArgs e)
		{
			var currentSelStart = richTextBox.SelectionStart;
		    var currentSelLength = richTextBox.SelectionLength;
		
		    richTextBox.SelectAll();
		    richTextBox.SelectionColor = SystemColors.WindowText;
		
		    var matches = Regex.Matches(richTextBox.Text, @"[\$].*?[\$]");
		    foreach (Match match in matches)
		    {
		        richTextBox.Select(match.Index, match.Length);
		        richTextBox.SelectionColor = Color.Blue;
		    }
		
		    richTextBox.Select(currentSelStart, currentSelLength);
		    richTextBox.SelectionColor = SystemColors.WindowText;
	
		}
		void OkBtnClick(object sender, EventArgs e)
		{
			cell.Value = richTextBox.Text;
			Close();
		}
		void CanselBtnClick(object sender, EventArgs e)
		{
			Close();	
		}
		
		
	}
}
