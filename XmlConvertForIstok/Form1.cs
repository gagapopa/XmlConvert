using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using  XmlConvertForIstok.Convert;

namespace XmlConvertForIstok
{
    public partial class Form1 : Form
    {
		private ExcelTexRead _read;
        private WriteXml _write;
        private Dictionary<string, int> _dictionary;
        private StationBilder _stationBilder = Station.Create();
        private Station _station;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Openbtn_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();
            OpentxtBox.Text = openFileDialog1.FileName;

            if (!(OpentxtBox.Text == "openFileDialog1"))
            {
                OpentxtBox.Text = openFileDialog1.FileName;
				_read = new ExcelTexRead(_stationBilder,OpentxtBox.Text);
                _dictionary = _read.GetDictionarySheets();
                SheetsListBox.DataSource = _dictionary.Keys.ToList();

                label2.Visible = true;
                SheetsListBox.Visible = true;
                ConvBtn.Visible = true;
            }
            OpentxtBox.Text = "";
        }

        private void ConvBtn_Click(object sender, EventArgs e)
        {
            
            saveFileDialog.ShowDialog();
            var fileName = saveFileDialog.FileName + ".xml";

            foreach (var item in SheetsListBox.Items)
            {
                if (SheetsListBox.CheckedItems.Contains(item))
                {
						_read.BildTable("НТЭЦ4",item.ToString(),TepOrManual.Manual);
//                    _read.ReadParameterValues(_stationBilder, _dictionary[item.ToString()]);
                }
                else
                {
						_read.BildTable("НТЭЦ4",item.ToString(),TepOrManual.Tep);
                } 
            }
            _station = _stationBilder;
            _write = new WriteXml(_station, fileName);
            _write.WriteXmlbySerealise();
            progressBar.Maximum = 100;
            progressBar.Value = 100;

        }
    }
}
