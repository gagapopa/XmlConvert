namespace XmlConvertForIstok
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Openbtn = new System.Windows.Forms.Button();
            this.OpentxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConvBtn = new System.Windows.Forms.Button();
            this.SheetsListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Openbtn
            // 
            this.Openbtn.Location = new System.Drawing.Point(12, 55);
            this.Openbtn.Name = "Openbtn";
            this.Openbtn.Size = new System.Drawing.Size(131, 23);
            this.Openbtn.TabIndex = 0;
            this.Openbtn.Text = "Открыть файл excel";
            this.Openbtn.UseVisualStyleBackColor = true;
            this.Openbtn.Click += new System.EventHandler(this.Openbtn_Click);
            // 
            // OpentxtBox
            // 
            this.OpentxtBox.Location = new System.Drawing.Point(12, 29);
            this.OpentxtBox.Name = "OpentxtBox";
            this.OpentxtBox.Size = new System.Drawing.Size(330, 20);
            this.OpentxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Путь к файлу";
            // 
            // ConvBtn
            // 
            this.ConvBtn.Location = new System.Drawing.Point(12, 362);
            this.ConvBtn.Name = "ConvBtn";
            this.ConvBtn.Size = new System.Drawing.Size(131, 23);
            this.ConvBtn.TabIndex = 3;
            this.ConvBtn.Text = "Конвертировать";
            this.ConvBtn.UseVisualStyleBackColor = true;
            this.ConvBtn.Visible = false;
            this.ConvBtn.Click += new System.EventHandler(this.ConvBtn_Click);
            // 
            // SheetsListBox
            // 
            this.SheetsListBox.FormattingEnabled = true;
            this.SheetsListBox.Location = new System.Drawing.Point(12, 112);
            this.SheetsListBox.Name = "SheetsListBox";
            this.SheetsListBox.Size = new System.Drawing.Size(197, 244);
            this.SheetsListBox.TabIndex = 4;
            this.SheetsListBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(289, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выберите Листы с ручным вводом";
            this.label2.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 431);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(330, 23);
            this.progressBar.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 466);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.SheetsListBox);
            this.Controls.Add(this.ConvBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpentxtBox);
            this.Controls.Add(this.Openbtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Openbtn;
        private System.Windows.Forms.TextBox OpentxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConvBtn;
        private System.Windows.Forms.CheckedListBox SheetsListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

