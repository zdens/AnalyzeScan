using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using AnalyzeScan.Controls;
//using System.Windows.Forms.DataVisualization.Charting;
using AnalyzeScan.Properties;
using LIMPFileReader;
using LIMPFileReader.Formats.Mzxml;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.WinForms;
using LiveCharts.Wpf;

namespace AnalyzeScan
{
    public static class DataShow
    {
        //private LimpXml scanData { get; set; }
        internal static void FillControlsWithData(Form1 form1, LimpXml msData)
        {
            if (msData != null)
            {
                form1.dataGridView1.Rows.Clear();
                var scanData = msData;
                ShowScanData(form1, "Scan path", msData.msRun.parentFile[0].fileName);
                ShowScanData(form1, "Creation date", msData.msRun.StartTime);
                ShowScanData(form1, "Comments", GetComments(msData.msRun.DataProcessing));
                ShowScanData(form1, "msModel", msData.msRun.MsInstrument.msModel.value);
                ShowScanData(form1, "Ionization", msData.msRun.MsInstrument.msIonisation.value);
                ShowScanData(form1, "MassAnalyzer", msData.msRun.MsInstrument.msMassAnalyzer.value);
                ShowScanData(form1, "Manufacturer", msData.msRun.MsInstrument.msManufacturer.value);
                ShowScanData(form1, "Scan count", msData.msRun.scanCount);
                ShowScanData(form1, "Polarity", msData.msRun.Scan[0].Polarity.ToString());
                form1.commentLabeledTextBox.text_box.Text = GetComments(msData.msRun.DataProcessing);
                form1.experimentLabeledTextBox.text_box.Text = msData.msRun.StartTime;
                //foreach (var param in Settings.Default.AdditionalParamsToShow)
                //{
                //    ShowScanData(form1, param, msData.msRun.Scan[0].GetAdditionalParams(param).ToString());
                //}
                foreach (var param in msData.msRun.Scan[0].AdditionalParameters)
                {
                    ShowScanData(form1, param.name, param.value);
                }


                PlotChart(form1, msData);
                if (form1.dataTableLayoutPanel.Height > form1.Height)
                    form1.dataTableLayoutPanel.AutoScroll = true;
            }
        }

        private static string GetComments(List<msRunDataProcessing> dataProcessing)
        {
            var str = string.Empty;
            foreach(var dp in dataProcessing)
            {
                str += string.Join("; ", dp.comment);
            }
            return str;
        }

        private static void ShowScanData(Form1 form1, string label, string data)
        {
            Label scanDataLabel = new Label() { Text = label, FlatStyle = FlatStyle.Popup, AutoSize = true, TextAlign = ContentAlignment.MiddleCenter, Padding = new Padding(2) };
            Label scanData = new Label() { Text = data, FlatStyle = FlatStyle.Popup, AutoSize = true, TextAlign = ContentAlignment.MiddleCenter, Padding = new Padding(2) };
            var rowControls = new Control[2];
            rowControls[0] = scanDataLabel;
            rowControls[1] = scanData;
            string[] row1 = new string[]{label, data};

            form1.dataGridView1.Rows.Add(row1);
            //AddRowToPanel(form1, form1.dataTableLayoutPanel, rowControls);
            //var item = new ListViewItem(label);
            //item.SubItems.Add(data);
            //form1.dataListView.Items.Add(item);
        }
        private static void PlotChart(Form1 form1, LimpXml msData)
        {
            form1.elementHost1.Visible = true;
            //form1.buttonResetZoom.Visible = true;
            form1.cartesianChart1.Series.Clear();
            //form1.cartesianChart1.DataTooltip.
            //form1.cartesianChart1.DataTooltip = new WpfControlLibrary1.CustomTooltipAndLegendExample(form1.cartesianChart1);
            Axis axisX, axisY;
            //var cartChart1 = form1.elementHost1.Controls.Find("cartesianChart1")
            axisX = GetAxis(form1.cartesianChart1, "X");
            axisY = GetAxis(form1.cartesianChart1, "Y");
            axisY.LabelFormatter = form1.GetLabelChartFormatFunc();
            
            axisY.MinValue = 0;
            //if (.AxisX.Count > 0)
            //    axisX = form1.cartesianChart1.AxisX[0];
            //else
            //    axisX = form1.cartesianChart1.AxisX[0];
            axisX.Title = "Retention time, s";
            axisY.Title = "TIC";
            axisY.LabelFormatter = form1.GetLabelYChartFormatFunc();
            var values = new LineSeries
            {
                Values = new ChartValues<ObservablePoint>(),
                Title = "TIC",
                PointGeometry = DefaultGeometries.None,
                LineSmoothness = 0,
            };
            //var series = new SeriesCollection{values};

            TabPage tabScansPage = new TabPage("Scans");
            TableLayoutPanel tblScans = CreateTable();
            tblScans.ColumnStyles.Clear();
            tblScans.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblScans.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tabScansPage.Controls.Add(tblScans);
            //ListView lvScans = new ListView
            //{
            //    LabelWrap = false,
            //    Dock = DockStyle.Fill,
            //    View = View.Details,
            //    HeaderStyle = ColumnHeaderStyle.None,
            //};
            //ColumnHeader header = new ColumnHeader() { Text = "Scan number", TextAlign = HorizontalAlignment.Left };
            
            //lvScans.Columns.Add(header);
            
            //lvScans.ItemSelectionChanged += scanSelected;


            TreeView tvScans = new TreeView()
            {
                Dock = DockStyle.Fill,
                ShowPlusMinus = true,
                Scrollable = true,
                
            };
            tvScans.NodeMouseDoubleClick += TvScans_NodeMouseDoubleClick;
            tblScans.Controls.Add(tvScans, 0, 0);
            foreach (var sc in msData.msRun.Scan)
            {
                var x = sc.RetentionTimeTs.TotalMinutes;
                var y = sc.totIonCurrent;
                values.Values.Add(new ObservablePoint(x, y));
                LimpTreeNode tNode = new LimpTreeNode()
                {
                    Text = string.Format("Scan {0}, time: {1}", sc.Num, sc.RetentionTimeTs),
                    Name = sc.Num.ToString(),
                    scanData = sc.Peaks.MzIntPeaks,
                    basePeakIntensity = sc.basePeakIntensity,
                    totIonCurrent = sc.totIonCurrent
                };
                //LimpListViewItem lviScan = new LimpListViewItem
                //{
                //    Text = string.Format("Scan {0}, time: {1}", sc.Num, sc.RetentionTimeTs),
                //    Name = sc.Num.ToString(),
                //    scanData = sc.Peaks.MzIntPeaks,
                //    basePeakIntensity = sc.basePeakIntensity,
                //    totIonCurrent = sc.totIonCurrent
                //};
                
                if (sc.ChildScan != null && sc.ChildScan.Any())
                {
                    //ColumnHeader subHeader = new ColumnHeader() { Text = "Additional info", TextAlign = HorizontalAlignment.Left };
                    //lvScans.Columns.Add(subHeader);

                    
                    foreach (var ssc in sc.ChildScan)
                    {
                        var sx = ssc.RetentionTimeTs.TotalMinutes;
                        var sy = ssc.totIonCurrent;
                        values.Values.Add(new ObservablePoint(sx, sy));
                        LimpTreeNode tSubNode = new LimpTreeNode()
                        {
                            Text = string.Format("Scan {0}, time: {1}", ssc.Num, ssc.RetentionTimeTs),
                            Name = ssc.Num.ToString(),
                            scanData = ssc.Peaks.MzIntPeaks,
                            basePeakIntensity = ssc.basePeakIntensity,
                            totIonCurrent = ssc.totIonCurrent
                        };
                        tNode.Nodes.Add(tSubNode);
                        //LimpListViewSubItem slviScan = new LimpListViewSubItem
                        //{
                        //    Text = string.Format("Scan {0}, time: {1}", ssc.Num, ssc.RetentionTimeTs),
                        //    Name = ssc.Num.ToString(),
                        //    scanData = ssc.Peaks.MzIntPeaks,
                        //    basePeakIntensity = ssc.basePeakIntensity,
                        //    totIonCurrent = ssc.totIonCurrent
                        //};
                        //lviScan.SubItems.Add(slviScan);
                        //lvScans.Items.Add(slviScan);
                    }
                    //lviScan.SubItems.AddRange(GetListViewSubItems(sc.ChildScan, values, true, lviScan).ToArray());
                }
                tvScans.Nodes.Add(tNode);
                //lvScans.Items.Add(lviScan);
            }

            //tblScans.Controls.Add(form1.elementHost2, 1, 0);
            tblScans.Controls.Add(form1.elementHost2, 1, 0);
            //lvScans.Items.AddRange(GetListViewItems(msData.msRun.Scan, ref values));
            
            //lvScans.Columns[0].Width = -1;
            form1.tabControl1.TabPages.Add(tabScansPage);
            form1.cartesianChart1.Series.Add(values);
        }

        private static void TvScans_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var tbNode = (LimpTreeNode)e.Node;
            var tblPanel = ((TreeView)sender).Parent;
            tblPanel.UseWaitCursor = true;
            System.Windows.Forms.Integration.ElementHost elh = (System.Windows.Forms.Integration.ElementHost)tblPanel.Controls[1];
            LiveCharts.Wpf.CartesianChart chart = (LiveCharts.Wpf.CartesianChart)elh.Child;

            chart.Series.Clear();
            Axis scanAxisX, scanAxisY;
            scanAxisX = GetAxis(chart, "X");
            scanAxisY = GetAxis(chart, "Y");
            scanAxisX.Title = "M/Z";
            scanAxisY.Title = "Intensity";
            scanAxisY.Separator = new Separator();
            scanAxisY.LabelFormatter = value => value.ToString();
            scanAxisX.Labels = new List<string>();

            var yValues = new ChartValues<ObservableValue>();
            var selectedScan = tbNode.scanData;// scanData.msRun.Scan.ElementAt(e.ItemIndex);
            var basePeakIntensity = tbNode.basePeakIntensity;
            var totIonCurrent = tbNode.totIonCurrent;
            foreach (DataPoint s in selectedScan)
            {
                if (s.Intensity == 0 || CheckRatio(s.Intensity, totIonCurrent, Settings.Default.IntensityCuttoffPercentage)) continue;
                //if (s.Intensity == 0) continue;
                yValues.Add(new ObservableValue(s.Intensity));
                scanAxisX.Labels.Add(s.Mz.ToString());
            }
            chart.Series.Add(new ColumnSeries
            {
                Values = yValues,
                LabelPoint = point => point.Y.ToString(),
                Title = "M/Z-Intentsity",
                MaxColumnWidth = 1,
                ColumnPadding = 0,
                //Configuration = GetMapper(basePeakIntensity),
                ToolTip = new DefaultTooltip()
            });
            chart.Zoom = ZoomingOptions.X;
            tblPanel.UseWaitCursor = false;
        }

        //private static ListViewItem[] GetListViewItems(List<Scan> scan, ref LineSeries values, bool subItems = false, ListViewItem owner = null)
        //{
        //    var lvScans = new List<LimpListViewItem>();
        //    //var lvSubScans = new List<ListViewItem.ListViewSubItem>();
        //    //var lvSubScans = new ListViewItem.ListViewSubItemCollection(owner);
        //    foreach (var sc in scan)
        //    {
        //        var x = sc.RetentionTimeTs.TotalMinutes;
        //        var y = sc.totIonCurrent;
        //        values.Values.Add(new ObservablePoint(x, y));
        //        LimpListViewItem lviScan = new LimpListViewItem
        //        {
        //            Text = string.Format("Scan {0}, time: {1}", sc.Num, sc.RetentionTimeTs),
        //            Name = sc.Num.ToString(),
        //            scanData = sc.Peaks.MzIntPeaks
        //        };
        //        lvScans.Add(lviScan);
        //        if (sc.ChildScan != null && sc.ChildScan.Any())
        //        {
        //            foreach(var ssc in sc.ChildScan)
        //            {
        //                var sx = sc.RetentionTimeTs.TotalMinutes;
        //                var sy = sc.totIonCurrent;
        //                values.Values.Add(new ObservablePoint(x, y));
        //                LimpListViewItem slviScan = new LimpListViewItem
        //                {
        //                    Text = string.Format("Scan {0}, time: {1}", sc.Num, sc.RetentionTimeTs),
        //                    Name = sc.Num.ToString(),
        //                    scanData = sc.Peaks.MzIntPeaks
        //                };
        //                lvScans.Add(slviScan);
        //            }
        //            //lviScan.SubItems.AddRange(GetListViewSubItems(sc.ChildScan, values, true, lviScan).ToArray());
        //        }


        //    }
        //    return lvScans.ToArray();
        //}

        //private static List<ListViewItem.ListViewSubItem> GetListViewSubItems(List<Scan> childScan, LineSeries values, bool v, LimpListViewItem lviScan)
        //{
        //    var lvSubScans = new List<ListViewItem.ListViewSubItem>();
        //    foreach (var sc in childScan)
        //    {
        //        var x = sc.RetentionTimeTs.TotalMinutes;
        //        var y = sc.totIonCurrent;
        //        values.Values.Add(new ObservablePoint(x, y));

        //        LimpListViewSubItem subScan = new LimpListViewSubItem
        //            {
        //                Text = string.Format("Sub scan {0}, time: {1}", sc.Num, sc.RetentionTimeTs),
        //                Name = sc.Num.ToString(),
        //                scanData = sc.Peaks.MzIntPeaks,
        //                basePeakIntensity = sc.basePeakIntensity,
        //                totIonCurrent = sc.totIonCurrent
        //            };
        //            lvSubScans.Add(subScan);

        //            // Assume there won't be examples with the msLevel > 2

        //            //if (sc.ChildScan.Any())
        //            //{
        //            //    lvSubScans.SubItems.AddRange((ListViewItem.ListViewSubItem[])GetListViewItems(sc.ChildScan, values, true));


        //    }
        //    return lvSubScans;
        //}

        //private static void scanSelected(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    if (e.IsSelected)
        //    {
                
        //        //MessageBox.Show("Selected scan " + e.Item.Name);
        //        var tblPanel = ((ListView)sender).Parent;
        //        tblPanel.UseWaitCursor = true;
        //        System.Windows.Forms.Integration.ElementHost elh = (System.Windows.Forms.Integration.ElementHost)tblPanel.Controls[1];
        //        LiveCharts.Wpf.CartesianChart chart = (LiveCharts.Wpf.CartesianChart)elh.Child;

        //        var scan = e.Item.SubItems;

        //        chart.Series.Clear();
        //        Axis scanAxisX, scanAxisY;
        //        scanAxisX = GetAxis(chart, "X");
        //        scanAxisY = GetAxis(chart, "Y");
        //        scanAxisX.Title = "M/Z";
        //        scanAxisY.Title = "Intensity";
        //        scanAxisY.Separator = new Separator();
        //        scanAxisY.LabelFormatter = value => value.ToString();
        //        scanAxisX.Labels = new List<string>();
                
        //        var yValues = new ChartValues<ObservableValue>();
        //        var selectedScan = ((LimpListViewItem)e.Item).scanData;// scanData.msRun.Scan.ElementAt(e.ItemIndex);
        //        var basePeakIntensity = ((LimpListViewItem)e.Item).basePeakIntensity;
        //        var totIonCurrent = ((LimpListViewItem)e.Item).totIonCurrent;
        //        foreach (DataPoint s in selectedScan)
        //        {
        //            if (s.Intensity == 0 || CheckRatio(s.Intensity, totIonCurrent, Settings.Default.IntensityCuttoffPercentage)) continue;
        //            //if (s.Intensity == 0) continue;
        //            yValues.Add(new ObservableValue(s.Intensity));
        //            scanAxisX.Labels.Add(s.Mz.ToString());
        //        }
        //        chart.Series.Add(new ColumnSeries
        //        {
        //            Values = yValues,
        //            LabelPoint = point => point.Y.ToString(),
        //            Title = "M/Z-Intentsity",
        //            MaxColumnWidth = 1,
        //            ColumnPadding = 0,
        //            Configuration = GetMapper(basePeakIntensity),
        //            ToolTip = new DefaultTooltip()
        //        });
        //        chart.Zoom = ZoomingOptions.X;
        //        tblPanel.UseWaitCursor = false;
        //    }
        //}

        private static bool CheckRatio(double intensity, double totIonCurrent, decimal intensityCuttoffPercentage)
        {
            return (decimal)(100f * intensity / totIonCurrent) < intensityCuttoffPercentage;
        }

        //private static CartesianMapper<ObservablePoint> GetMapper()
        //{
        //    var dangerBrush = new SolidColorBrush(Colors.Red);
        //    var common = new SolidColorBrush(Colors.Black);
        //    var mapper = Mappers.Xy<ObservablePoint>()
        //        .X(item => item.X)
        //        .Y(item => item.Y)
        //        .Fill(item => item.X == basePeakMz ? dangerBrush : common);
        //    return mapper;
        //}
        private static CartesianMapper<ObservableValue> GetMapper(double basePeakIntensity)
        {
            var dangerBrush = new SolidColorBrush(Colors.DarkRed);
            var common = new SolidColorBrush(Colors.Black);
            var mapper = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill(item => item.Value == basePeakIntensity ? dangerBrush : common)
                .Stroke(item => item.Value == basePeakIntensity ? dangerBrush : common);
            return mapper;
        }

        private static TableLayoutPanel CreateTable()
        {
            var table = new TableLayoutPanel
            {
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize,
                Location = new Point(3, 3),
                Name = "tablescan1",
                RowCount = 2
            };
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            
            table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            return table;
        }
        private static Axis GetAxis(LiveCharts.Wpf.CartesianChart cartesianChart1, string v)
        {
            Axis axis = new Axis();
            switch (v)
            {
                case "X":
                    if (cartesianChart1.AxisX.Count > 0)
                        return cartesianChart1.AxisX[0];
                    else
                        cartesianChart1.AxisX.Add(axis);
                    break;
                case "Y":
                    if (cartesianChart1.AxisY.Count > 0)
                        return cartesianChart1.AxisY[0];
                    else
                        cartesianChart1.AxisY.Add(axis);
                    break;
                default:
                    break;
            }
            return axis;
        }
        //private static void ClearZoom(LiveCharts.WinForms.CartesianChart cartesianChart1)
        //{
        //    //to clear the current zoom/pan just set the axis limits to double.NaN

            
        //}
        private static void AddRowToPanel(Form1 form1, TableLayoutPanel panel, Control[] rowElements)
        {
            if (panel.ColumnCount != rowElements.Length)
                throw new Exception("Elements number doesn't match!");
            //get a reference to the previous existent row
            RowStyle rowStyle;
            ColumnStyle colStyle1, colStyle2;
            //if (panel.RowCount == 0)
            //{
            //    rowStyle = new RowStyle() { SizeType = SizeType.AutoSize };
            //    colStyle1 = new ColumnStyle() { SizeType = SizeType.AutoSize };
            //    colStyle2 = new ColumnStyle() { SizeType = SizeType.Percent, Width = 100 };
            //}
            //else
            //{
            //    rowStyle = panel.RowStyles[panel.RowCount - 1];
            //    colStyle1 = panel.ColumnStyles[panel.RowCount - 1];
            //    colStyle2 = panel.ColumnStyles[panel.RowCount - 1];
            //}

            rowStyle = new RowStyle() { SizeType = SizeType.AutoSize };
            colStyle1 = new ColumnStyle() { SizeType = SizeType.AutoSize };
            colStyle2 = new ColumnStyle() { SizeType = SizeType.AutoSize };
            //increase panel rows count by one
            panel.RowCount++;
            //form1.tableLayoutPanel1.SetRowSpan(panel, form1.tableLayoutPanel1.GetRowSpan(panel) + 1);
            for (int i = 0; i < rowElements.Length; i++)
            {
                panel.Controls.Add(rowElements[i], i, panel.RowCount - 1);
                if (rowElements[i].Height > rowStyle.Height)
                    rowStyle.Height = rowElements[i].Height;
            }            //panel.SetRowSpan(panel.GetRowSpan())
            //add a new RowStyle as a copy of the previous one
            panel.RowStyles.Add(new RowStyle(rowStyle.SizeType, rowStyle.Height));
            //panel.ColumnStyles.Add(new ColumnStyle(colStyle.SizeType, colStyle.Width));
            panel.ColumnStyles.Add(colStyle1);
            panel.ColumnStyles.Add(colStyle2);
            //add the control

        }

        
    }
}
