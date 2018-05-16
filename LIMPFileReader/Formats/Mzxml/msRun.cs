using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("msRun")]
    public  class msRun
    {
        DateTimeStyles style = DateTimeStyles.RoundtripKind;
        CultureInfo culture = CultureInfo.InvariantCulture;
        #region Private
        private List<msRunParentFile> parentFileField;

        private msRunMsInstrument msInstrumentField;

        
        private List<msRunDataProcessing> dataProcessingField;

        
        private List<separationTechniqueType> separationField;

        private msRunSpotting spottingField;

        
        private List<Scan> scanField;

        private string sha1Field;

        private string scanCountField;

        private DateTime startTimeField;

        private DateTime endTimeField;
        #endregion
        public msRun()
        {
            //this.scanField = new List<Scan>();
            //this.spottingField = new msRunSpotting();
            //this.separationField = new List<separationTechniqueType>();
            this.dataProcessingField = new List<msRunDataProcessing>();
            this.msInstrumentField = new msRunMsInstrument();
            this.parentFileField = new List<msRunParentFile>();
        }

        [XmlElement("parentFile", IsNullable = false)]
        public List<msRunParentFile> parentFile
        {
            get
            {
                return this.parentFileField;
            }
            set
            {
                this.parentFileField = value;
            }
        }
        [XmlElement("msInstrument", IsNullable = false)]
        public msRunMsInstrument MsInstrument
        {
            get
            {
                return this.msInstrumentField;
            }
            set
            {
                this.msInstrumentField = value;
            }
        }
        [XmlElement("dataProcessing", IsNullable = false)]
        public List<msRunDataProcessing> DataProcessing
        {
            get
            {
                return this.dataProcessingField;
            }
            set
            {
                this.dataProcessingField = value;
            }
        }

        [XmlArrayItem("separationTechnique", IsNullable = true)]
        public List<separationTechniqueType> Separation
        {
            get
            {
                return this.separationField;
            }
            set
            {
                this.separationField = value;
            }
        }
        [XmlElement("spotting")]
        [DefaultValue(null)]
        public msRunSpotting Spotting
        {
            get
            {
                return this.spottingField;
            }
            set
            {
                this.spottingField = value;
            }
        }
        //public bool ShouldSerializeAge()
        //{
        //    return Age.HasValue;
        //}
        [XmlElement("scan")]
        public List<Scan> Scan
        {
            get
            {
                return this.scanField;
            }
            set
            {
                this.scanField = value;
            }
        }
        [XmlElement("sha1")]
        public string sha1
        {
            get
            {
                return this.sha1Field;
            }
            set
            {
                this.sha1Field = value;
            }
        }
        [XmlAttribute("scanCount")]
        public string scanCount
        {
            get
            {
                return Scan.Count.ToString();
                //if (string.IsNullOrEmpty(this.scanCountField))
                //return this.scanCountField;
            }
            set
            {
                this.scanCountField = value;
            }
        }
        [XmlAttribute("startTime")]
        public string StartTime
        {
            get
            {
                return this.startTimeField.ToString(new CultureInfo("en-us"));
            }
            set
            {
                if (value.Contains("+"))
                {
                    this.startTimeField = DateTime.ParseExact(value.Split(new[] { '+' })[0], "yyyyMMddHHmmss", null);
                }
                else
                    if (!DateTime.TryParse(value, culture, style, out startTimeField))
                        this.startTimeField = DateTime.MinValue;
            }
        }
        [XmlAttribute("endTime")]
        public string EndTime
        {
            get
            {
                return this.endTimeField.ToString(new CultureInfo("en-us"));
            }
            set
            {
                if (value.Contains("+"))
                {
                    this.startTimeField = DateTime.ParseExact(value.Split(new[] { '+' })[0], "yyyyMMddHHmmss", null);
                }
                else
                    if (!DateTime.TryParse(value, culture, style, out endTimeField))
                        this.endTimeField = DateTime.MinValue;
            }
        }

        public DateTime StartTimeDt { get
            {
                return this.startTimeField;
            }
        }
    }
}
