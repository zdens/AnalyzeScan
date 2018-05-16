using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("index")]
    public class mzXMLIndex
    {

        private List<mzXMLIndexOffset> offsetField;

        private string nameField;

        public mzXMLIndex()
        {
            this.offsetField = new List<mzXMLIndexOffset>();
        }

        [XmlElement("offset", typeof(mzXMLIndexOffset))]
        public List<mzXMLIndexOffset> offset
        {
            get
            {
                return this.offsetField;
            }
            set
            {
                this.offsetField = value;
            }
        }
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
    }
}
