using System;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("scanOrigin")]
    public partial class scanScanOrigin
    {

        private string parentFileIDField;

        private string numField;

        [XmlAttribute("parentFileID")]
        public string ParentFileID
        {
            get
            {
                return this.parentFileIDField;
            }
            set
            {
                this.parentFileIDField = value;
            }
        }
        [XmlAttribute("num")]
        public string Num
        {
            get
            {
                return this.numField;
            }
            set
            {
                this.numField = value;
            }
        }
    }
}
