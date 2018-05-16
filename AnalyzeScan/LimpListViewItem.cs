using LIMPFileReader.Formats.Mzxml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalyzeScan
{
    public class LimpListViewItem : ListViewItem
    {
        public List<DataPoint> scanData { get; set; }
        public double basePeakIntensity { get; set; }
        public double totIonCurrent { get; set; }
        public LimpListViewItem()
        {
            scanData = new List<DataPoint>();
        }
    }
    public class LimpListViewSubItem : ListViewItem.ListViewSubItem
    {
        public List<DataPoint> scanData { get; set; }
        public double basePeakIntensity { get; set; }
        public double totIonCurrent { get; set; }
        public LimpListViewSubItem()
        {
            scanData = new List<DataPoint>();
        }
    }
}
