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
using System.Windows.Forms.DataVisualization.Charting;
using AnalyzeScan.Properties;
using AnalyzeScan.Tools;
using LIMPFileReader;
using LiveCharts.Defaults;
using NLog;
using NLog.Targets;
using NLog.Config;



namespace AnalyzeScan
{
    public partial class Form1 : Form
    {
        public LimpXml msData;
        Point? prevPosition = null;

        private Func<double, string> labelChartFormatFunc = (x) => string.Format("{0:0.0000}", x);
        private Func<double, string> labelYChartFormatFunc = (x) => string.Format("{0:0.0#E+0}", x);

        public Logger Logger { get; set; }

        public Func<double, string> GetLabelChartFormatFunc()
        {
            return labelChartFormatFunc;
        }

        public void SetLabelChartFormatFunc(Func<double, string> value)
        {
            labelChartFormatFunc = value;
        }
        public Func<double, string> GetLabelYChartFormatFunc()
        {
            return labelYChartFormatFunc;
        }

        public void SetLabelYChartFormatFunc(Func<double, string> value)
        {
            labelYChartFormatFunc = value;
        }

        public Form1()
        {
            
            
            
            InitializeComponent();
            elementHost1.Visible = false;
            
            var fileTarget = new FileTarget
            {
                ArchiveFileName = "${basedir}/archives/log.{#####}.txt",
                ArchiveAboveSize = 1024000,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                KeepFileOpen = false,
                FileName = @"OperationalLog.txt"
            };
            var config = new LoggingConfiguration();
            config.AddTarget("file", fileTarget);
            var rule2 = new LoggingRule("*", LogLevel.Info, fileTarget);
            var excRule = new LoggingRule("${longdate} ${message} ${exception:format=tostring}", LogLevel.Error, fileTarget);
            config.LoggingRules.Add(rule2);
            config.LoggingRules.Add(excRule);
            LogManager.Configuration = config;
            Logger = LogManager.GetLogger("Common");
#if !DEBUG
            //var fp = @"c:\Users\den\temp\2'_MeOH_neg_after_drying_auto-msms.raw";
            var fp = @"c:\Users\den\temp\INTACT_747.d\Analysis.yep";
            var fData = new MsData(fp, Logger, Settings.Default.ReportData);
            if (string.IsNullOrWhiteSpace(fData.StatusMessage))
            {
                AddFields();
                ShowData();
                msData = (LimpXml)fData.ScanData;
                DataShow.FillControlsWithData(this, msData);
            }
            else
            {
                MessageBox.Show(fData.StatusMessage, "Error", MessageBoxButtons.OK);
            }
#endif

            //cartesianChart1.AxisX.Clear();
            //cartesianChart1.AxisY.Clear();
            //CheckFormats(Settings.Default.EnableCdf);
            //CdfFormat.PrepareCdf();
        }


        //private void toticChart_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var pos = e.Location;
        //    if (prevPosition.HasValue && pos == prevPosition.Value)
        //        return;
        //    toolTip1.RemoveAll();
        //    prevPosition = pos;
        //    var results = totIonCurrentChart.HitTest(pos.X, pos.Y, false,
        //                                    ChartElementType.DataPoint);
        //    foreach (var result in results)
        //    {
        //        if (result.ChartElementType == ChartElementType.DataPoint)
        //        {
        //            var prop = result.Object as DataPoint;
        //            if (prop != null)
        //            {
        //                var pointXPixel = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
        //                var pointYPixel = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
        //                toolTip1.Show("Time=" + pointXPixel + ", TIC=" + pointYPixel, this.totIonCurrentChart,
        //                                    pos.X, pos.Y - 15);

        //                //var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
        //                // check if the cursor is really close to the point (2 pixels around)
        //                //if (Math.Abs(pos.X - pointXPixel) < 20 &&
        //                //    Math.Abs(pos.Y - pointYPixel) < 20)

        //            }
        //        }
        //    }
        //}

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = string.Empty;
            openFileDialog1.Filter = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ShowData();
                AddFields();
            }
        }

        private void AddFields()
        {
            // 
            // experimentLabeledTextBox
            // 
            this.experimentLabeledTextBox.Location = new System.Drawing.Point(3, 3);
            this.experimentLabeledTextBox.Name = "experimentLabeledTextBox";
            this.experimentLabeledTextBox.Size = new System.Drawing.Size(100, 20);
            this.experimentLabeledTextBox.TabIndex = 0;
            this.experimentLabeledTextBox.Dock = DockStyle.Fill;
            // 
            // commentLabeledTextBox
            // 
            this.commentLabeledTextBox.Location = new System.Drawing.Point(3, 3);
            this.commentLabeledTextBox.Name = "commentLabeledTextBox";
            this.commentLabeledTextBox.Size = new System.Drawing.Size(100, 20);
            this.commentLabeledTextBox.TabIndex = 0;
            this.commentLabeledTextBox.Dock = DockStyle.Fill;
            this.tableLayoutPanel2.Controls.Add(this.experimentLabeledTextBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.commentLabeledTextBox, 0, 1);
        }

        private void openScanButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripOpenExistingButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = string.Empty;
            openFileDialog1.Filter = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AddFields();
                ShowData();
                
            }
        }

        private void ShowData()
        {
            ClearTable(dataTableLayoutPanel);
            scansChart.Series.Clear();
            if (tabControl1.TabPages.Count > 1)
                tabControl1.TabPages.RemoveAt(1);
            var fData = new MsData(openFileDialog1.FileName, Logger, Settings.Default.LimpReportData);
            if (string.IsNullOrWhiteSpace(fData.StatusMessage))
            {
                msData = (LimpXml)fData.ScanData;
                DataShow.FillControlsWithData(this, msData);
            }
            else
            {
                MessageBox.Show(fData.StatusMessage, "Error", MessageBoxButtons.OK);
                RemoveFields();
            }
        }

        private void RemoveFields()
        {
            for(int i = 0; i < this.tableLayoutPanel2.Controls.Count; i++)
            {
                this.tableLayoutPanel2.Controls.RemoveAt(i);
            }
        }

        private static void ClearTable(TableLayoutPanel dataTableLayoutPanel)
        {
            for (int rowIndex = dataTableLayoutPanel.RowCount - 1; rowIndex >= 0; rowIndex--)
            {
                for (int col = 0; col < dataTableLayoutPanel.ColumnCount; col++)
                {
                    var ctrl = dataTableLayoutPanel.GetControlFromPosition(col, rowIndex);
                    if (ctrl != null)
                    {
                        dataTableLayoutPanel.Controls.RemoveByKey(ctrl.Name);
                        ctrl.Dispose();
                    }
                    //dataTableLayoutPanel.Controls.Remove(ctrl);
                }
                dataTableLayoutPanel.RowStyles.RemoveAt(rowIndex);
                dataTableLayoutPanel.RowCount -= 1;
            }

        }

        private void toolStripConnectDeviceButton_Click(object sender, EventArgs e)
        {
            var data = new MsData();
        }

        private void totIonCurrentChart_Click(object sender, EventArgs e)
        {

        }

        private void buttonResetZoom_Click(object sender, EventArgs e)
        {
            cartesianChart1.AxisX[0].MinValue = double.NaN;
            cartesianChart1.AxisX[0].MaxValue = double.NaN;
            cartesianChart1.AxisY[0].MinValue = double.NaN;
            cartesianChart1.AxisY[0].MaxValue = double.NaN;
        }

        private void buttonSaveToPdf_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".pdf";
            saveFileDialog1.Filter = Settings.Default.SaveAsPdfFilter;
            saveFileDialog1.CheckFileExists = false;
            var dataToExport = new Exports(msData, this);

            //var res = Exports.ToPdf(dataToExport, saveFileDialog1.FileName, this);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var res = Exports.ToPdf(dataToExport, saveFileDialog1.FileName, this, Logger);
                if (!res.Equals("OK"))
                    MessageBox.Show(res, "Error", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Export is finished", "Ready", MessageBoxButtons.OK);
            }
            
        }

        private void buttonSaveMzXml_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".mzxml";
            saveFileDialog1.Filter = Settings.Default.SaveAsMzXmlFilter;
            saveFileDialog1.CheckFileExists = false;
            //openFileDialog1.file
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var res = Exports.ToMzXml(msData, saveFileDialog1.FileName);
                if (!res.Equals("OK"))
                    MessageBox.Show(res, "Error", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Export is finished", "Ready", MessageBoxButtons.OK);
            }
        }
    }

}