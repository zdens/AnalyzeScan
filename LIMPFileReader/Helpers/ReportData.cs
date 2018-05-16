using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMPFileReader.Helpers
{
    public class ReportData
    {
        public List<AdditionalReportData> Data { get; set; }
        public ReportData()
        {
            Data = new List<AdditionalReportData>();
        }
    }
}
