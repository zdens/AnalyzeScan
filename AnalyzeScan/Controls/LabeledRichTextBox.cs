using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AnalyzeScan.Controls
{
    public class LabeledRichTextBox : FlowLayoutPanel
    {
        public Label label;
        public RichTextBox text_box;
        private TableLayoutPanel tbl;

        public LabeledRichTextBox(string label_text)
            : base()
        {
            AutoSize = true;
            tbl = new TableLayoutPanel();
            tbl.ColumnCount = 1;
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tbl.RowCount = 2;
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            label = new Label();
            label.Text = label_text;
            label.AutoSize = true;
            label.Anchor = AnchorStyles.Left;
            label.TextAlign = ContentAlignment.MiddleLeft;
            text_box = new RichTextBox();
            text_box.Dock = DockStyle.Bottom;

            tbl.Controls.Add(label, 0, 0);
            tbl.Controls.Add(text_box, 0, 1);
            Controls.Add(tbl);
        }
    }
}
