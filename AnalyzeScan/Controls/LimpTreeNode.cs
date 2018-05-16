using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIMPFileReader.Formats.Mzxml;

namespace AnalyzeScan.Controls
{
    public class LimpTreeNode : TreeNode
    {
        public List<DataPoint> scanData { get; set; }
        public double basePeakIntensity { get; set; }
        public double totIonCurrent { get; set; }
        public LimpTreeNode()
        {
            scanData = new List<DataPoint>();
        }
    }
}
