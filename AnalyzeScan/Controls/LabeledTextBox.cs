using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeScan.Controls
{
    public class LabeledTextBox : FlowLayoutPanel
    {
        public Label label;
        public TextBox text_box;

        public LabeledTextBox(string label_text)
            : base()
        {
            AutoSize = true;

            label = new Label();
            label.Text = label_text;
            label.AutoSize = true;
            label.Anchor = AnchorStyles.Left;
            label.TextAlign = ContentAlignment.MiddleLeft;

            Controls.Add(label);

            text_box = new TextBox();
            text_box.Anchor = AnchorStyles.Right;

            Controls.Add(text_box);
        }
    }
}
