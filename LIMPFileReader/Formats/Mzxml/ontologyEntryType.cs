using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class ontologyEntryType
    {

        private string categoryField;

        private string valueField;

        [XmlAttribute("category")]
        public string category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }
        [XmlAttribute("value")]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}
