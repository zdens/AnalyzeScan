using System;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("software")]
    public class Software
    {
        private softwareType typeField;

        private string nameField;

        private string versionField;

        private System.DateTime completionTimeField;

        private bool completionTimeFieldSpecified;

        private string valueField;

        [XmlAttribute("type")]
        public softwareType Type
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
        [XmlAttribute("name")]
        public string Name
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
        [XmlAttribute("version")]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
        [XmlAttribute]
        public DateTime completionTime
        {
            get
            {
                return this.completionTimeField;
            }
            set
            {
                this.completionTimeField = value;
            }
        }

        [XmlIgnore()]
        public bool completionTimeSpecified
        {
            get
            {
                return this.completionTimeFieldSpecified;
            }
            set
            {
                this.completionTimeFieldSpecified = value;
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
