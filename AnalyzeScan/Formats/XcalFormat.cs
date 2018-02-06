using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyzeScan.Tools;
using XCALIBURFILESLib;

namespace AnalyzeScan.Formats
{
    public class XcalFormat : FormatsCheck
    {
        private string fileName;

        public string LastMessage { get; set; }
        public int status { get; set; } /* Returned status code. */
        public long id { get; set; }
        public string FileVersion { get; set; }
        public XRaw Raw { get; set; }

        public XcalFormat()
        {
            Check();
        }

        public XcalFormat(string fileName)
        {
            this.fileName = fileName;
            ReadFile(fileName);
        }

        public void Check()
        {
            Description = "uknown";
            //Raw = new XRaw();
            //Raw.
        }

        public void ReadFile(string fileName)
        {
            Raw = new XRaw();
            try
            {
                Raw.Open(fileName);
                var info = Raw.RawInfo;
                Description = info.ToString();
            }
            catch (Exception e)
            {
                LastMessage = e.Message;
            }
        }
    }
}