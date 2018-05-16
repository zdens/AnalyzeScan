using System.Collections.Generic;

namespace LIMPFileReader.Formats.Bruker
{
    internal class ParametersComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            if (string.IsNullOrEmpty(x))
                return false;
            if (string.IsNullOrEmpty(y))
                return false;
            var parts = x.Split(new[] { ',', ';' });
            return parts[0].Equals(y) | parts[1].Equals(y);
        }

        public int GetHashCode(string obj)
        {
            if (obj == null)
                return 0;
            return obj.GetHashCode();
        }
    }
}