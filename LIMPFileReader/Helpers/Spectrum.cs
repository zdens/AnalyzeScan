using System.Collections.Generic;

namespace LIMPFileReader.Helpers
{
    public class Spectrum
    {
        public List<double> Masses { get; set; }
        public List<double> Intensities { get; set; }

        public Spectrum()
        {
            Masses = new List<double>();
            Intensities = new List<double>();
        }
    }
}
