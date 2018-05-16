using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("scan")]
    public partial class Scan
    {
        #region private fields
        private List<scanScanOrigin> scanOriginField;

        private List<scanPrecursorMz> precursorMzField;

        private ScanMaldi maldiField;

        private Peaks peaksField;

        private List<namevalueType> nameValueField;

        private List<string> commentField;

        private List<Scan> scan1Field;

        private int numField;

        private string msLevelField;

        private int peaksCountField;

        private scanPolarity polarityField;

        private bool polarityFieldSpecified;

        private scanScanType scanTypeField;

        private bool scanTypeFieldSpecified;

        private int centroidedField;

        private bool centroidedFieldSpecified;

        private bool deisotopedField;

        private bool deisotopedFieldSpecified;

        private bool chargeDeconvolutedField;

        private string retentionTimeField;

        private float ionisationEnergyField;

        private bool ionisationEnergyFieldSpecified;

        private float collisionEnergyField;

        private bool collisionEnergyFieldSpecified;

        private float cidGasPressureField;

        private bool cidGasPressureFieldSpecified;

        private float startMzField;

        private bool startMzFieldSpecified;

        private float endMzField;

        private bool endMzFieldSpecified;

        private double lowMzField;

        private bool lowMzFieldSpecified;

        private double highMzField;

        private bool highMzFieldSpecified;

        private double basePeakMzField;

        private bool basePeakMzFieldSpecified;

        private double basePeakIntensityField;

        private bool basePeakIntensityFieldSpecified;

        private double totIonCurrentField;

        private bool totIonCurrentFieldSpecified;
        #endregion

        public Scan()
        {
            this.peaksField = new Peaks();
            AdditionalParameters = new List<namevalueType>();
            //this.scan1Field = new List<Scan>();
            //this.commentField = new List<string>();
            //this.nameValueField = new List<namevalueType>();

            //this.precursorMzField = new List<scanPrecursorMz>();
            //this.scanOriginField = new List<scanScanOrigin>();
            //this.chargeDeconvolutedField = false;
        }
        #region Elements
        [XmlElement("scanOrigin")]
        public List<scanScanOrigin> ScanOrigin
        {
            get
            {
                return this.scanOriginField;
            }
            set
            {
                this.scanOriginField = value;
            }
        }
        [XmlElement("precursorMz")]
        public List<scanPrecursorMz> PrecursorMz
        {
            get
            {
                return this.precursorMzField;
            }
            set
            {
                this.precursorMzField = value;
            }
        }
        [XmlElement("maldi")]
        public ScanMaldi Maldi
        {
            get
            {
                return this.maldiField;
            }
            set
            {
                this.maldiField = value;
            }
        }
        [XmlElement("peaks")]
        public Peaks Peaks
        {
            get
            {
                return this.peaksField;
            }
            set
            {
                this.peaksField = value;
            }
        }
        [XmlElement("nameValue")]
        public List<namevalueType> AdditionalParameters
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
        [XmlElement("comment")]
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
        [XmlElement("scan")]
        public List<Scan> ChildScan
        {
            get
            {
                return this.scan1Field;
            }
            set
            {
                this.scan1Field = value;
            }
        }
        #endregion

        #region Attributes
        [XmlAttribute("num")]
        public int Num
        {
            get
            {
                return this.numField;
            }
            set
            {
                this.numField = value;
            }
        }
        [XmlAttribute("msLevel")]
        public string MsLevel
        {
            get
            {
                return this.msLevelField;
            }
            set
            {
                this.msLevelField = value;
            }
        }
        [XmlAttribute("peaksCount")]
        public int PeaksCount
        {
            get
            {
                return this.peaksCountField;
            }
            set
            {
                this.peaksCountField = value;
            }
        }
        [XmlAttribute("polarity")]
        public scanPolarity Polarity
        {
            get
            {
                return this.polarityField;
            }
            set
            {
                this.polarityField = value;
                polarityFieldSpecified = true;
            }
        }


        

        [XmlAttribute("scanType")]
        public scanScanType ScanType
        {
            get
            {
                return this.scanTypeField;
            }
            set
            {
                this.scanTypeField = value;
            }
        }

        [XmlIgnore()]
        public bool scanTypeSpecified
        {
            get
            {
                return this.scanTypeFieldSpecified;
            }
            set
            {
                this.scanTypeFieldSpecified = value;
            }
        }
        [XmlAttribute("centroided")]
        public int Centroided
        {
            get
            {
                return this.centroidedField;
            }
            set
            {
                this.centroidedField = value;
                centroidedFieldSpecified = true;
            }
        }

        [XmlIgnore()]
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
        [XmlAttribute("deisotoped")]
        public bool Deisotoped
        {
            get
            {
                return this.deisotopedField;
            }
            set
            {
                this.deisotopedField = value;
                if (value)
                    deisotopedFieldSpecified = true;
            }
        }

        [XmlIgnore()]
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

        [System.ComponentModel.DefaultValue(false)]
        [XmlAttribute("chargeDeconvoluted")]
        public bool ChargeDeconvoluted
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
        [XmlAttribute("retentionTime")]
        public string retentionTime
        {
            get
            {
                return this.retentionTimeField;
            }
            set
            {
                this.retentionTimeField = value;
            }
        }
        [XmlIgnore()]
        public TimeSpan RetentionTimeTs
        {
            get
            {
                return XmlConvert.ToTimeSpan(retentionTimeField);
            }
            set
            {
                //rtAsTimeSpan = value;
                retentionTimeField = XmlConvert.ToString(value);
            }
        }
        [XmlAttribute("ionisationEnergy")]
        public float ionisationEnergy
        {
            get
            {
                return this.ionisationEnergyField;
            }
            set
            {
                this.ionisationEnergyField = value;
                if (value > 0)
                    ionisationEnergyFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool ionisationEnergySpecified
        {
            get
            {
                return this.ionisationEnergyFieldSpecified;
            }
            set
            {
                this.ionisationEnergyFieldSpecified = value;
            }
        }
        [XmlAttribute("collisionEnergy")]
        public float collisionEnergy
        {
            get
            {
                return this.collisionEnergyField;
            }
            set
            {
                this.collisionEnergyField = value;
            }
        }

        [XmlIgnore()]
        public bool collisionEnergySpecified
        {
            get
            {
                return this.collisionEnergyFieldSpecified;
            }
            set
            {
                this.collisionEnergyFieldSpecified = value;
            }
        }
        [XmlAttribute("cidGasPressure")]
        public float cidGasPressure
        {
            get
            {
                return this.cidGasPressureField;
            }
            set
            {
                this.cidGasPressureField = value;
                if (value > 0)
                    cidGasPressureFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool cidGasPressureSpecified
        {
            get
            {
                return this.cidGasPressureFieldSpecified;
            }
            set
            {
                this.cidGasPressureFieldSpecified = value;
            }
        }
        [XmlAttribute("startMz")]
        public float startMz
        {
            get
            {
                return this.startMzField;
            }
            set
            {
                this.startMzField = value;
                if (value > 0)
                    startMzFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool startMzSpecified
        {
            get
            {
                return this.startMzFieldSpecified;
            }
            set
            {
                this.startMzFieldSpecified = value;
            }
        }
        [XmlAttribute("endMz")]
        public float endMz
        {
            get
            {
                return this.endMzField;
            }
            set
            {
                this.endMzField = value;
                if (value > 0)
                    endMzFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool endMzSpecified
        {
            get
            {
                return this.endMzFieldSpecified;
            }
            set
            {
                this.endMzFieldSpecified = value;
            }
        }
        [XmlAttribute("lowMz")]
        public double lowMz
        {
            get
            {
                return this.lowMzField;
            }
            set
            {
                this.lowMzField = value;
                if (value > 0)
                    lowMzFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool lowMzSpecified
        {
            get
            {
                return this.lowMzFieldSpecified;
            }
            set
            {
                this.lowMzFieldSpecified = value;
            }
        }
        [XmlAttribute("highMz")]
        public double highMz
        {
            get
            {
                return this.highMzField;
            }
            set
            {
                this.highMzField = value;
                if (value > 0)
                    highMzFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool highMzSpecified
        {
            get
            {
                return this.highMzFieldSpecified;
            }
            set
            {
                this.highMzFieldSpecified = value;
            }
        }
        [XmlAttribute("basePeakMz")]
        public double basePeakMz
        {
            get
            {
                return this.basePeakMzField;
            }
            set
            {
                basePeakMzField = value;
                if (value > 0)
                    basePeakMzFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool basePeakMzSpecified
        {
            get
            {
                return this.basePeakMzFieldSpecified;
            }
            set
            {
                this.basePeakMzFieldSpecified = value;
            }
        }
        [XmlAttribute("basePeakIntensity")]
        public double basePeakIntensity
        {
            get
            {
                return this.basePeakIntensityField;
            }
            set
            {
                this.basePeakIntensityField = value;
                if (value > 0)
                    basePeakIntensityFieldSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool basePeakIntensitySpecified
        {
            get
            {
                return this.basePeakIntensityFieldSpecified;
            }
            set
            {
                this.basePeakIntensityFieldSpecified = value;
            }
        }
        [XmlAttribute("totIonCurrent")]
        public double totIonCurrent
        {
            get
            {
                return this.totIonCurrentField;
            }
            set
            {
                this.totIonCurrentField = value;
                if (value > 0)
                    this.totIonCurrentSpecified = true;
            }
        }

        [XmlIgnore()]
        public bool totIonCurrentSpecified
        {
            get
            {
                return this.totIonCurrentFieldSpecified;
            }
            set
            {
                this.totIonCurrentFieldSpecified = value;
            }
        }
        [XmlIgnore()]
        public bool polaritySpecified
        {
            get
            {
                return this.polarityFieldSpecified;
            }
            set
            {
                this.polarityFieldSpecified = value;
            }
        }
        #endregion

        internal void FindBasePeak()
        {
            var lst = new List<DataPoint>(Peaks.MzIntPeaks);
            lst.Sort(new DataPointIntensityComparison());
            var maxPeak = lst.ToArray()[lst.Count - 1];
            basePeakMz = maxPeak.Mz;
            basePeakIntensity = maxPeak.Intensity;
        }
        internal void FindLowHighMz()
        {
            var lst = new List<DataPoint>(Peaks.MzIntPeaks);
            lst.Sort(new DataPointMzComparison());
            var maxPeak = lst.ToArray()[lst.Count - 1];
            var minPeak = lst.ToArray()[0];
            highMz = maxPeak.Mz;
            lowMz = minPeak.Mz;
        }
        public object GetAdditionalParams(string paramName)
        {
            object value = "Not specified";
            if (AdditionalParameters.Count > 0)
            {
                var param = AdditionalParameters.Find(p => p.name == paramName);
                value = param.value;
            }
            return value;
        }
    }
}
