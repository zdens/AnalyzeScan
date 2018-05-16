using System.Collections.Generic;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunDataProcessing
    {
        
        private Software softwareField;

        private List<processingOperation> processingOperationField;

        private string[] commentField;

        private float intensityCutoffField;

        private bool intensityCutoffFieldSpecified;

        private bool centroidedField;

        private bool centroidedFieldSpecified;

        private bool deisotopedField;

        private bool deisotopedFieldSpecified;

        private bool chargeDeconvolutedField;

        private bool chargeDeconvolutedFieldSpecified;

        private bool spotIntegrationField;

        private bool spotIntegrationFieldSpecified;

        public msRunDataProcessing()
        {
            //this.commentField = new string[]();
            this.processingOperationField = new List<processingOperation>();
            this.softwareField = new Software();
        }

        [XmlElement("software")]
        public Software software
        {
            get
            {
                return this.softwareField;
            }
            set
            {
                this.softwareField = value;
            }
        }

        [XmlElement("processingOperation")]
        public List<processingOperation> processingOperation
        {
            get
            {
                return this.processingOperationField;
            }
            set
            {
                this.processingOperationField = value;
            }
        }

        [XmlElement("comment")]
        public string[] comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }

        public float intensityCutoff
        {
            get
            {
                return this.intensityCutoffField;
            }
            set
            {
                this.intensityCutoffField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool intensityCutoffSpecified
        {
            get
            {
                return this.intensityCutoffFieldSpecified;
            }
            set
            {
                this.intensityCutoffFieldSpecified = value;
            }
        }

        public bool centroided
        {
            get
            {
                return this.centroidedField;
            }
            set
            {
                this.centroidedField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool centroidedSpecified
        {
            get
            {
                return this.centroidedFieldSpecified;
            }
            set
            {
                this.centroidedFieldSpecified = value;
            }
        }

        public bool deisotoped
        {
            get
            {
                return this.deisotopedField;
            }
            set
            {
                this.deisotopedField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deisotopedSpecified
        {
            get
            {
                return this.deisotopedFieldSpecified;
            }
            set
            {
                this.deisotopedFieldSpecified = value;
            }
        }

        public bool chargeDeconvoluted
        {
            get
            {
                return this.chargeDeconvolutedField;
            }
            set
            {
                this.chargeDeconvolutedField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool chargeDeconvolutedSpecified
        {
            get
            {
                return this.chargeDeconvolutedFieldSpecified;
            }
            set
            {
                this.chargeDeconvolutedFieldSpecified = value;
            }
        }

        public bool spotIntegration
        {
            get
            {
                return this.spotIntegrationField;
            }
            set
            {
                this.spotIntegrationField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool spotIntegrationSpecified
        {
            get
            {
                return this.spotIntegrationFieldSpecified;
            }
            set
            {
                this.spotIntegrationFieldSpecified = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">name attribute</param>
        /// <param name="v2">value attribute</param>
        /// <param name="v3">type attribute, can be null</param>
        public void StoreDataProcessing(string v1, string v2, string v3 = null)
        {
            var po = new processingOperation { name = v1, value = v2 };
            if (!string.IsNullOrWhiteSpace(v3))
                po.type = v3;
            processingOperation.Add(po);
        }
    }
}
