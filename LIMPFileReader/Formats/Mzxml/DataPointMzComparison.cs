using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMPFileReader.Formats.Mzxml
{
    internal class DataPointMzComparison : Comparer<DataPoint>
    {
        public override int Compare(DataPoint x, DataPoint y)
        {
            if (x != null && y != null)
            {
                return x.Mz.CompareTo(y.Mz);
            }
            if (y != null && x == null)
            {
                return y.Mz.CompareTo(0);
            }
            if (x != null && y == null)
                return x.Mz.CompareTo(0);
            return 0;
        }
    }
}
