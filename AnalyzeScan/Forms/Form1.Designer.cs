using AnalyzeScan.Controls;
using AnalyzeScan.Properties;
using System.Windows.Forms;

namespace AnalyzeScan
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.cartesianChart1 = new LiveCharts.Wpf.CartesianChart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabStatusPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSaveToPdf = new System.Windows.Forms.Button();
            this.buttonSaveToMzXml = new System.Windows.Forms.Button();
            this.patientTextBox = new System.Windows.Forms.TextBox();
            this.medCommentsTextBox = new System.Windows.Forms.TextBox();
            this.buttonResetZoom = new System.Windows.Forms.Button();
            this.dataTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripOpenExistingButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripConnectDeviceButton = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.scansChart = new LiveCharts.Wpf.CartesianChart();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tissueTextBox = new System.Windows.Forms.TextBox();
            this.commentLabeledTextBox = new LabeledRichTextBox("Comments");
            this.experimentLabeledTextBox = new LabeledTextBox("Experiment date");
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabStatusPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(323, 4);
            this.elementHost1.Name = "elementHost1";
            this.tableLayoutPanel1.SetRowSpan(this.elementHost1, 2);
            this.elementHost1.Size = new System.Drawing.Size(951, 765);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.cartesianChart1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1292, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileOpenToolStripMenuItem
            // 
            this.fileOpenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";
            this.fileOpenToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileOpenToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.openFileToolStripMenuItem.Text = "Open file";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabStatusPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1292, 805);
            this.tabControl1.TabIndex = 1;
            // 
            // tabStatusPage
            // 
            this.tabStatusPage.Controls.Add(this.tableLayoutPanel1);
            this.tabStatusPage.Location = new System.Drawing.Point(4, 22);
            this.tabStatusPage.Name = "tabStatusPage";
            this.tabStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatusPage.Size = new System.Drawing.Size(1284, 779);
            this.tabStatusPage.TabIndex = 0;
            this.tabStatusPage.Text = "Data info";
            this.tabStatusPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.elementHost1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1278, 773);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 390);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(312, 379);
            this.dataGridView1.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            this.Column2.Width = 59;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonSaveToPdf, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.buttonSaveToMzXml, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.patientTextBox, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.medCommentsTextBox, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tissueTextBox, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.30577F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07436F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.27276F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.27276F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.53718F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.53718F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(312, 379);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // buttonSaveToPdf
            // 
            this.buttonSaveToPdf.Location = new System.Drawing.Point(3, 309);
            this.buttonSaveToPdf.Name = "buttonSaveToPdf";
            this.buttonSaveToPdf.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveToPdf.TabIndex = 0;
            this.buttonSaveToPdf.Text = "Save as pdf";
            this.buttonSaveToPdf.UseVisualStyleBackColor = true;
            this.buttonSaveToPdf.Click += new System.EventHandler(this.buttonSaveToPdf_Click);
            // 
            // buttonSaveToMzXml
            // 
            this.buttonSaveToMzXml.Location = new System.Drawing.Point(3, 349);
            this.buttonSaveToMzXml.Name = "buttonSaveToMzXml";
            this.buttonSaveToMzXml.Size = new System.Drawing.Size(93, 23);
            this.buttonSaveToMzXml.TabIndex = 0;
            this.buttonSaveToMzXml.Text = "Save as MzXml";
            this.buttonSaveToMzXml.UseVisualStyleBackColor = true;
            this.buttonSaveToMzXml.Click += new System.EventHandler(this.buttonSaveMzXml_Click);
            // 
            // patientTextBox
            // 
            this.patientTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.patientTextBox.Location = new System.Drawing.Point(3, 143);
            this.patientTextBox.Name = "patientTextBox";
            this.patientTextBox.Size = new System.Drawing.Size(218, 20);
            this.patientTextBox.TabIndex = 1;
            this.patientTextBox.Text = "Patient Id";
            // 
            // medCommentsTextBox
            // 
            this.medCommentsTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.medCommentsTextBox.Location = new System.Drawing.Point(3, 206);
            this.medCommentsTextBox.Multiline = true;
            this.medCommentsTextBox.Name = "medCommentsTextBox";
            this.medCommentsTextBox.Size = new System.Drawing.Size(218, 46);
            this.medCommentsTextBox.TabIndex = 2;
            this.medCommentsTextBox.Text = "Medical comments";
            // 
            // buttonResetZoom
            // 
            this.buttonResetZoom.Location = new System.Drawing.Point(1150, 150);
            this.buttonResetZoom.Name = "buttonResetZoom";
            this.buttonResetZoom.Size = new System.Drawing.Size(75, 23);
            this.buttonResetZoom.TabIndex = 1;
            this.buttonResetZoom.Text = "Reset Zoom";
            this.buttonResetZoom.UseVisualStyleBackColor = true;
            this.buttonResetZoom.Visible = false;
            this.buttonResetZoom.Click += new System.EventHandler(this.buttonResetZoom_Click);
            // 
            // dataTableLayoutPanel
            // 
            this.dataTableLayoutPanel.AutoScroll = true;
            this.dataTableLayoutPanel.AutoSize = true;
            this.dataTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataTableLayoutPanel.BackColor = System.Drawing.Color.PapayaWhip;
            this.dataTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.dataTableLayoutPanel.ColumnCount = 2;
            this.dataTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.dataTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.dataTableLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.dataTableLayoutPanel.Name = "dataTableLayoutPanel";
            this.dataTableLayoutPanel.RowCount = 1;
            this.dataTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dataTableLayoutPanel.Size = new System.Drawing.Size(5, 2);
            this.dataTableLayoutPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpenExistingButton,
            this.toolStripConnectDeviceButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1292, 54);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripOpenExistingButton
            // 
            this.toolStripOpenExistingButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOpenExistingButton.Image")));
            this.toolStripOpenExistingButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripOpenExistingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOpenExistingButton.Name = "toolStripOpenExistingButton";
            this.toolStripOpenExistingButton.Size = new System.Drawing.Size(67, 51);
            this.toolStripOpenExistingButton.Text = "Open scan";
            this.toolStripOpenExistingButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripOpenExistingButton.Click += new System.EventHandler(this.toolStripOpenExistingButton_Click);
            // 
            // toolStripConnectDeviceButton
            // 
            this.toolStripConnectDeviceButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripConnectDeviceButton.Image")));
            this.toolStripConnectDeviceButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripConnectDeviceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripConnectDeviceButton.Name = "toolStripConnectDeviceButton";
            this.toolStripConnectDeviceButton.Size = new System.Drawing.Size(102, 51);
            this.toolStripConnectDeviceButton.Text = "Scan from device";
            this.toolStripConnectDeviceButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripConnectDeviceButton.Click += new System.EventHandler(this.toolStripConnectDeviceButton_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 10;
            this.toolTip1.IsBalloon = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1MinSize = 67;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1292, 892);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // elementHost2
            // 
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(0, 0);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(509, 406);
            this.elementHost2.TabIndex = 0;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.scansChart;
            // 
            // experimentLabeledTextBox
            // 
            this.experimentLabeledTextBox.AutoSize = true;
            this.experimentLabeledTextBox.Location = new System.Drawing.Point(0, 0);
            this.experimentLabeledTextBox.Name = "experimentLabeledTextBox";
            this.experimentLabeledTextBox.Size = new System.Drawing.Size(200, 100);
            this.experimentLabeledTextBox.TabIndex = 0;
            // 
            // commentLabeledTextBox
            // 
            this.commentLabeledTextBox.AutoSize = true;
            this.commentLabeledTextBox.Location = new System.Drawing.Point(0, 0);
            this.commentLabeledTextBox.Name = "commentLabeledTextBox";
            this.commentLabeledTextBox.Size = new System.Drawing.Size(200, 100);
            this.commentLabeledTextBox.TabIndex = 0;
            // 
            // tissueTextBox
            // 
            this.tissueTextBox.Location = new System.Drawing.Point(3, 269);
            this.tissueTextBox.Name = "tissueTextBox";
            this.tissueTextBox.Size = new System.Drawing.Size(100, 20);
            this.tissueTextBox.TabIndex = 3;
            this.tissueTextBox.Text = "Tissue";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 916);
            this.Controls.Add(this.buttonResetZoom);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = global::AnalyzeScan.Properties.Settings.Default.Name;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabStatusPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Integration.ElementHost elementHost1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabStatusPage;
        public System.Windows.Forms.ToolStripContainer toolStripContainer1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton toolStripOpenExistingButton;
        public System.Windows.Forms.ToolStripButton toolStripConnectDeviceButton;
        //public System.Windows.Forms.DataVisualization.Charting.Chart totIonCurrentChart;
        public ToolTip toolTip1;
        public SplitContainer splitContainer1;
        public TableLayoutPanel tableLayoutPanel1;
        public TableLayoutPanel dataTableLayoutPanel;
        public LiveCharts.Wpf.CartesianChart cartesianChart1;
        public DataGridView dataGridView1;
        private ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.Button buttonResetZoom;
        public LiveCharts.Wpf.CartesianChart scansChart;
        public System.Windows.Forms.Integration.ElementHost elementHost2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private TableLayoutPanel tableLayoutPanel2;
        public LabeledTextBox experimentLabeledTextBox;
        public LabeledRichTextBox commentLabeledTextBox;
        private Button buttonSaveToPdf;
        private Button buttonSaveToMzXml;
        private SaveFileDialog saveFileDialog1;
        public TextBox patientTextBox;
        public TextBox medCommentsTextBox;
        public TextBox tissueTextBox;
    }
}

