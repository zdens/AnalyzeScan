using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("msInstrument")]
    public partial class msRunMsInstrument
    {

        private msRunMsInstrumentMsManufacturer msManufacturerField;

        private ontologyEntryType msModelField;

        private ontologyEntryType msIonisationField;

        private msRunMsInstrumentMsMassAnalyzer msMassAnalyzerField;

        private MsDetector msDetectorField;

        private Software softwareField;

        private ontologyEntryType msResolutionField;

        private @operator operatorField;

        private List<namevalueType> nameValueField;

        private List<string> commentField;

        public msRunMsInstrument()
        {
            this.commentField = new List<string>();
            this.nameValueField = new List<namevalueType>();
            //this.operatorField = new @operator();
            //this.msResolutionField = new ontologyEntryType();
            this.softwareField = new Software();
            this.msDetectorField = new MsDetector();
            this.msMassAnalyzerField = new msRunMsInstrumentMsMassAnalyzer();
            this.msIonisationField = new ontologyEntryType();
            this.msModelField = new ontologyEntryType();
            this.msManufacturerField = new msRunMsInstrumentMsManufacturer();
        }
        [XmlElement]
        public msRunMsInstrumentMsManufacturer msManufacturer
        {
            get
            {
                return this.msManufacturerField;
            }
            set
            {
                this.msManufacturerField = value;
            }
        }
        [XmlElement]
        public ontologyEntryType msModel
        {
            get
            {
                return this.msModelField;
            }
            set
            {
                this.msModelField = value;
            }
        }
        [XmlElement]
        public ontologyEntryType msIonisation
        {
            get
            {
                return this.msIonisationField;
            }
            set
            {
                this.msIonisationField = value;
            }
        }
        [XmlElement]
        public msRunMsInstrumentMsMassAnalyzer msMassAnalyzer
        {
            get
            {
                return this.msMassAnalyzerField;
            }
            set
            {
                this.msMassAnalyzerField = value;
            }
        }
        [XmlElement]
        public MsDetector msDetector
        {
            get
            {
                return this.msDetectorField;
            }
            set
            {
                this.msDetectorField = value;
            }
        }
        [XmlElement]
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
        [XmlElement]
        public ontologyEntryType msResolution
        {
            get
            {
                return this.msResolutionField;
            }
            set
            {
                this.msResolutionField = value;
            }
        }
        [XmlElement]
        public @operator @operator
        {
            get
            {
                return this.operatorField;
            }
            set
            {
                this.operatorField = value;
            }
        }
        [XmlElement]
        public List<namevalueType> nameValue
        {
            get
            {
                return this.nameValueField;
            }
            set
            {
                this.nameValueField = value;
            }
        }
        [XmlElement]
        public List<string> comment
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
    }
}
