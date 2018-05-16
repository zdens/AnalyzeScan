using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class namevalueType
    {

        private string nameField;

        private string valueField;

        private string typeField;

        private string valueField1;

        [XmlAttribute("name")]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
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
        [XmlAttribute("type")]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return this.valueField1;
            }
            set
            {
                this.valueField1 = value;
            }
        }
    }
}
