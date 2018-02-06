using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalyzeScan.Formats;
using AnalyzeScan.Properties;
using AnalyzeScan.Tools;
using CDF;

namespace AnalyzeScan
{
    public partial class Form1 : Form
    {
        private CdfFormat _cdfFormat;
        private XcalFormat _xcalFormat;

        public Form1()
        {
            InitializeComponent();
            CheckFormats(Settings.Default.EnableCdf);
            //CdfFormat.PrepareCdf();
        }

        private void CheckFormats(bool enabled)
        {
            if (enabled)
            {
                checkBoxEnableCdf.Text = Resources.Form1_CheckFormats_Enabled;
                _cdfFormat = new CdfFormat();
                labelCdfDescription.BackColor = _cdfFormat.Enabled ? Color.GreenYellow : Color.Tomato;

                labelCdfDescription.Text = _cdfFormat.Description;
            }
            else
            {
                checkBoxEnableCdf.Text = Resources.Form1_CheckFormats_Disabled;
                labelCdfDescription.BackColor = Color.Tomato;
                labelCdfDescription.Text = $"CDF disabled in the application";
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (Path.GetExtension(openFileDialog1.FileName)?.ToLower())
                {
                    case ".cdf":
                        
                        if (!_cdfFormat.Enabled)
                        {
                            MessageBox.Show("CDF is not enabled");
                            return;
                        }

                        tabCdfPage.Show();
                        _cdfFormat.ReadFile(openFileDialog1.FileName);
                        labelCdfFile.Text = _cdfFormat.id > 0 ? _cdfFormat.FileVersion : _cdfFormat.LastMessage;
                        break;
                    case ".raw":
                        if (!Settings.Default.EnableXcalibur)
                        {
                            MessageBox.Show("XCalibur is not enabled");
                            return;
                        }
                        tabXcaliburPage.Show();
                        _xcalFormat = new XcalFormat(openFileDialog1.FileName);
                        labelXcaliburStatus.Text = _xcalFormat.Description;
                        break;
                }
            }
        }

        private void checkBoxEnableCdf_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            Helpers.AddUpdateAppSettings("EnableCdf", checkBox.Checked.ToString());
            CheckFormats(checkBox.Checked);
        }
    }
}
