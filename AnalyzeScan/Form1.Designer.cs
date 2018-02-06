using AnalyzeScan.Properties;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabStatusPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxEnableCdf = new System.Windows.Forms.CheckBox();
            this.labelCdfDescription = new System.Windows.Forms.Label();
            this.tabCdfPage = new System.Windows.Forms.TabPage();
            this.labelCdfFile = new System.Windows.Forms.Label();
            this.tabXcaliburPage = new System.Windows.Forms.TabPage();
            this.labelXcaliburStatus = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabStatusPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCdfPage.SuspendLayout();
            this.tabXcaliburPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
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
            this.tabControl1.Controls.Add(this.tabCdfPage);
            this.tabControl1.Controls.Add(this.tabXcaliburPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(629, 439);
            this.tabControl1.TabIndex = 1;
            // 
            // tabStatusPage
            // 
            this.tabStatusPage.Controls.Add(this.tableLayoutPanel1);
            this.tabStatusPage.Location = new System.Drawing.Point(4, 22);
            this.tabStatusPage.Name = "tabStatusPage";
            this.tabStatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatusPage.Size = new System.Drawing.Size(621, 413);
            this.tabStatusPage.TabIndex = 0;
            this.tabStatusPage.Text = "Status";
            this.tabStatusPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxEnableCdf, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelCdfDescription, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(615, 407);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.tableLayoutPanel1.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(301, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "CDF";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxEnableCdf
            // 
            this.checkBoxEnableCdf.AutoSize = true;
            this.checkBoxEnableCdf.Checked = global::AnalyzeScan.Properties.Settings.Default.EnableCdf;
            this.checkBoxEnableCdf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableCdf.Location = new System.Drawing.Point(310, 3);
            this.checkBoxEnableCdf.Name = "checkBoxEnableCdf";
            this.checkBoxEnableCdf.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnableCdf.TabIndex = 1;
            this.checkBoxEnableCdf.Text = "Enabled";
            this.checkBoxEnableCdf.UseVisualStyleBackColor = true;
            this.checkBoxEnableCdf.CheckedChanged += new System.EventHandler(this.checkBoxEnableCdf_CheckedChanged);
            // 
            // labelCdfDescription
            // 
            this.labelCdfDescription.AutoSize = true;
            this.labelCdfDescription.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelCdfDescription.Location = new System.Drawing.Point(310, 25);
            this.labelCdfDescription.Name = "labelCdfDescription";
            this.labelCdfDescription.Size = new System.Drawing.Size(102, 25);
            this.labelCdfDescription.TabIndex = 2;
            this.labelCdfDescription.Text = "CDF Status uknown";
            this.labelCdfDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabCdfPage
            // 
            this.tabCdfPage.Controls.Add(this.labelCdfFile);
            this.tabCdfPage.Location = new System.Drawing.Point(4, 22);
            this.tabCdfPage.Name = "tabCdfPage";
            this.tabCdfPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabCdfPage.Size = new System.Drawing.Size(621, 413);
            this.tabCdfPage.TabIndex = 1;
            this.tabCdfPage.Text = "CDF";
            this.tabCdfPage.UseVisualStyleBackColor = true;
            // 
            // labelCdfFile
            // 
            this.labelCdfFile.AutoSize = true;
            this.labelCdfFile.Location = new System.Drawing.Point(9, 23);
            this.labelCdfFile.Name = "labelCdfFile";
            this.labelCdfFile.Size = new System.Drawing.Size(0, 13);
            this.labelCdfFile.TabIndex = 0;
            // 
            // tabXcaliburPage
            // 
            this.tabXcaliburPage.Controls.Add(this.labelXcaliburStatus);
            this.tabXcaliburPage.Location = new System.Drawing.Point(4, 22);
            this.tabXcaliburPage.Name = "tabXcaliburPage";
            this.tabXcaliburPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabXcaliburPage.Size = new System.Drawing.Size(621, 413);
            this.tabXcaliburPage.TabIndex = 2;
            this.tabXcaliburPage.Text = "XCalibur";
            this.tabXcaliburPage.UseVisualStyleBackColor = true;
            // 
            // labelXcaliburStatus
            // 
            this.labelXcaliburStatus.AutoSize = true;
            this.labelXcaliburStatus.Location = new System.Drawing.Point(22, 30);
            this.labelXcaliburStatus.Name = "labelXcaliburStatus";
            this.labelXcaliburStatus.Size = new System.Drawing.Size(0, 13);
            this.labelXcaliburStatus.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 463);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = global::AnalyzeScan.Properties.Settings.Default.Name;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabStatusPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabCdfPage.ResumeLayout(false);
            this.tabCdfPage.PerformLayout();
            this.tabXcaliburPage.ResumeLayout(false);
            this.tabXcaliburPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabStatusPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxEnableCdf;
        private System.Windows.Forms.TabPage tabCdfPage;
        private System.Windows.Forms.Label labelCdfDescription;
        private System.Windows.Forms.Label labelCdfFile;
        private System.Windows.Forms.TabPage tabXcaliburPage;
        private System.Windows.Forms.Label labelXcaliburStatus;
    }
}

