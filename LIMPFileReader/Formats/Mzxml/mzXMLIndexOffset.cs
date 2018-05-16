using System;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("offset")]
    public class mzXMLIndexOffset
    {
        private int idField;

        private long valueField;

        [XmlAttribute("id")]
        public int id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        [XmlText()]
        public string Value
        {
            get
            {
                return this.valueField.ToString();
            }
            set
            {
                this.valueField = Convert.ToInt64(value);
            }
        }
    }
}
