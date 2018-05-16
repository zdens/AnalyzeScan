namespace LIMPFileReader.Formats.Mzxml
{
    public enum softwareType
    {

        /// <remarks/>
        acquisition,

        /// <remarks/>
        conversion,

        /// <remarks/>
        processing,
    }

    public enum scanPeaksPrecision
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("32")]
        Item32,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("64")]
        Item64,
    }

    public enum scanPolarity
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("+")]
        Positive,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("-")]
        Negative,

        /// <remarks/>
        Unknown,
    }

    public enum scanScanType
    {

        /// <remarks/>
        Full,

        /// <remarks/>
        zoom,

        /// <remarks/>
        SIM,

        /// <remarks/>
        SRM,

        /// <remarks/>
        CRM,

        /// <remarks/>
        Q1,

        /// <remarks/>
        Q3,

        Unknown
    }
    public enum DetectorType
    {
        MS_electron_multiplier = 1000253,
        MS_photomultiplier = 1000116,
        //MS_EM = MS_electron_multiplier,
        //MS_PMT = MS_photomultiplier,
        MS_Unknown = 0
    }
}
