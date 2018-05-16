using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeScan.Report
{
    public interface IPdfMergeData
    {
        IDictionary<string, string> MergeFieldValues { get; }
    }
}
