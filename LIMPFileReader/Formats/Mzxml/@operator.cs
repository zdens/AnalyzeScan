using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class @operator
    {

        private string firstField;

        private string lastField;

        private string phoneField;

        private string emailField;

        private string uRIField;

        private string valueField;
        [XmlAttribute]
        public string first
        {
            get
            {
                return this.firstField;
            }
            set
            {
                this.firstField = value;
            }
        }
        [XmlAttribute]
        public string last
        {
            get
            {
                return this.lastField;
            }
            set
            {
                this.lastField = value;
            }
        }
        [XmlAttribute]
        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        [XmlAttribute]
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
        [XmlAttribute]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }

        [XmlText()]
        public string Value
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
