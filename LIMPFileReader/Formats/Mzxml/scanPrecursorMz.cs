using System;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("precursorMz")]
    public class scanPrecursorMz
    {

        private string precursorScanNumField;

        private float precursorIntensityField;

        private string precursorChargeField;

        private float windowWidenessField;

        private bool windowWidenessFieldSpecified;

        private string valueField;

        [XmlAttribute]
        public string precursorScanNum
        {
            get
            {
                return this.precursorScanNumField;
            }
            set
            {
                this.precursorScanNumField = value;
            }
        }
        [XmlAttribute]
        public float precursorIntensity
        {
            get
            {
                return this.precursorIntensityField;
            }
            set
            {
                this.precursorIntensityField = value;
            }
        }
        [XmlAttribute]
        public string precursorCharge
        {
            get
            {
                return this.precursorChargeField;
            }
            set
            {
                this.precursorChargeField = value;
            }
        }
        [XmlAttribute]
        public float windowWideness
        {
            get
            {
                return this.windowWidenessField;
            }
            set
            {
                this.windowWidenessField = value;
            }
        }

        [XmlIgnore()]
        public bool windowWidenessSpecified
        {
            get
            {
                return this.windowWidenessFieldSpecified;
            }
            set
            {
                this.windowWidenessFieldSpecified = value;
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
