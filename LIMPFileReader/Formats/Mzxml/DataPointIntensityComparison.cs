using System.Collections.Generic;
using LIMPFileReader.Formats.Mzxml;

namespace LIMPFileReader.Formats.Mzxml
{
    internal class DataPointIntensityComparison : Comparer<DataPoint>
    {
        public override int Compare(DataPoint x, DataPoint y)
        {
            if (x != null && y != null)
            {
                return x.Intensity.CompareTo(y.Intensity);
            }
            if (y != null && x == null)
            {
                return y.Intensity.CompareTo(0);
            }
            if (x != null && y == null)
                return x.Intensity.CompareTo(0);
            return 0;
        }
    }
}