using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LIMPFileReader.Formats.Mzxml;
using MSFileReaderLib;


using NLog;

namespace LIMPFileReader.Formats.Thermo
{
    [XmlRoot("mzXML", Namespace = "http://sashimi.sourceforge.net/schema_revision/mzXML_2.1", IsNullable = false)]
    public class Raw : LimpXml
    {
        #region Raw parameters
        IXRawfile5 rawFile;
        int nControllerType = 0; // 0 == mass spec device
        int nContorllerNumber = 1; // first MS device
        int totalNumScans = 0;      // Number of scans
        int firstScanNumber = 0, lastScanNumber = 0;   // Number of first and last scan
        string scanFilter = null;                     // Scan filter line
        //char polarity;                                // Polarity
        int msLevel = -1;                             // MS level
        int numDataPoints = -1; // points in both the m/z and intensity arrays
        double retentionTimeInMinutes = -1;
        double minObservedMZ = -1;
        double maxObservedMZ = -1;
        double totalIonCurrent = -1;
        double basePeakMZ = -1;
        double basePeakIntensity = -1;
        int channel = 0; // unused
        int uniformTime = 0; // unused
        double frequency = 0; // unused
        
        /// <value>Precursors</value>
        object precursorMz = null;      
        object precursorCharge = null;

        // Scan raw data points
        int arraySize = -1;
        object rawData = null; // rawData wil come as Double[,]
        object peakFlags = null;

        object pvarLabels = null;
        int pnArraySize = -1;
        int pnArraySize1 = -1;

        string szFilter = null;        // No filter
        int intensityCutoffType = 0;        // No cutoff
        int intensityCutoffValue = 0;    // No cutoff
        int maxNumberOfPeaks = 0;        // 0 : return all data peaks
        double centroidPeakWidth = 0;        // No centroiding
        int centroidThisScan = 0; // No centroiding

        bool isCentroided = false;

        string acquisitionDate = string.Empty;
        DateTime creationDate = DateTime.MinValue;
        string comment1 = string.Empty;
        string comment2 = string.Empty;

        private enum ThermoMzAnalyzer
        {
            None = -1,
            ITMS = 0,
            TQMS = 1,
            SQMS = 2,
            TOFMS = 3,
            FTMS = 4,
            Sector = 5
        }
        public enum DissociationType
        {
            UnKnown = -1,
            None = 6,
            CID = 0,
            HCD = 5,
            ETD = 4,
            MPD = 1,
            ECD = 2,
            PQD = 3,
            SA = 7,
            PTR = 8,
            NETD = 9,
            NPTR = 10,
            CI = 11
        }
        #endregion

        /// <value>Creation date</value>
        [XmlIgnore()]
        public DateTime CreationDate { get { return creationDate; } }
        byte[] byteBuffer { get ; set; }

        [XmlIgnore()]
        public string LastMessage { get; set; }

        public Raw(string filePath, Logger logger)
        {
            rawFile = (IXRawfile5)new MSFileReader_XRawfile();
            //var otherFile = new MS
            rawFile.Open(filePath);

            rawFile.SetCurrentController(nControllerType, nContorllerNumber);
            rawFile.GetNumSpectra(ref totalNumScans);

            rawFile.GetFirstSpectrumNumber(ref firstScanNumber);
            rawFile.GetLastSpectrumNumber(ref lastScanNumber);
            rawFile.GetCreationDate(ref creationDate);
            comment1 = null;
            comment2 = null;
            rawFile.GetComment1(ref comment1);
            rawFile.GetComment2(ref comment2);

            msRun.parentFile.Add(new msRunParentFile() { fileName = filePath });
            msRun.StartTime = creationDate.ToString(new CultureInfo("en-us"));
            msRun.scanCount = totalNumScans.ToString();
            msRun.Scan = new List<Scan>();
            index.name = "scan";
            index.offset = new List<mzXMLIndexOffset>();
            GetMsRunMetaInfo(msRun, rawFile, firstScanNumber);
            msRun.DataProcessing[0].StoreDataProcessing("Count of Scans", lastScanNumber.ToString());
            for (int curScanNum = firstScanNumber; curScanNum <= lastScanNumber; curScanNum++)
            {
                Scan curScan = new Scan();
                index.offset.Add(new mzXMLIndexOffset { id = curScanNum });
                scanFilter = null;
                rawFile.GetFilterForScanNum(curScanNum, ref scanFilter);
                curScan.Polarity = GetPolarity(scanFilter, logger);
                
                
                if (!LastMessage.Equals("OK"))
                {
                    rawFile.Close();
                    return;
                }

                //GetIonization(msRun, scanFilter);
                rawFile.GetMSOrderForScanNum(curScanNum, ref msLevel);
                if (msLevel < -1) msLevel = 2; // e.g., neutral gain scan returns -3, see MSFileReader doc
                if (msLevel < 1) msLevel = 1; // e.g., parent scan scan returns -1, see MSFileReader doc
                //Console.Write("MS LEVEL: " + msLevel + "\n");

                GetScanHeaderInformation(curScan, curScanNum);

                if (curScanNum == firstScanNumber)
                {
                    msRun.DataProcessing[0].StoreDataProcessing("Ion Polarity", curScan.Polarity.ToString());
                    msRun.DataProcessing[0].StoreDataProcessing("Start Polarity", curScan.Polarity.ToString());
                    msRun.DataProcessing[0].StoreDataProcessing("Stop Polarity", curScan.Polarity.ToString());
                    msRun.DataProcessing[0].StoreDataProcessing("Ready Polarity", curScan.Polarity.ToString());
                    msRun.DataProcessing[0].StoreDataProcessing("Scan Begin", curScan.lowMz.ToString());
                    msRun.DataProcessing[0].StoreDataProcessing("Scan End", curScan.highMz.ToString());
                }

                curScan.ScanType = GetScanType(rawFile, curScanNum);
                
                logger.Trace("from ScanHeader:");
                logger.Trace("tic = {0}, base mz = {1}, base inten = {2}", curScan.totIonCurrent, curScan.basePeakMz, curScan.basePeakIntensity);
                if (msLevel > 1)
                {
                    precursorMz = null;
                    precursorCharge = null;
                    rawFile.GetTrailerExtraValueForScanNum(curScanNum, "Monoisotopic M/Z:", ref precursorMz);
                    rawFile.GetTrailerExtraValueForScanNum(curScanNum, "Charge State:", ref precursorCharge);
                    curScan.PrecursorMz = new List<scanPrecursorMz>() {
                        new scanPrecursorMz() {
                            precursorCharge = precursorCharge.ToString(),
                            precursorScanNum = curScanNum.ToString(),
                            Value = precursorMz.ToString() } };
                }

                //int k;
                //object MassData = null;
                try
                {
                    //rawFile.GetMassListFromRT(ref retentionTimeInMinutes, scanFilter, intensityCutoffType, intensityCutoffValue, maxNumberOfPeaks, centroidThisScan, centroidPeakWidth, ref MassData, ref peakFlags, ref pnArraySize);
                    //rawFile.GetMassListFromRT(ref retentionTimeInMinutes, null, intensityCutoffType, intensityCutoffValue, maxNumberOfPeaks, centroidThisScan, centroidPeakWidth, ref MassData, ref peakFlags, ref pnArraySize);
                    rawData = null;
                    peakFlags = null;
                    centroidPeakWidth = 0;
                    arraySize = 0;
                    rawFile.GetMassListFromScanNum(
                                                    ref curScanNum,
                                                    szFilter,             // filter
                                                    intensityCutoffType, // intensityCutoffType
                                                    intensityCutoffValue, // intensityCutoffValue
                                                    maxNumberOfPeaks,     // maxNumberOfPeaks
                                                    centroidThisScan,        // centroid result?
                                                    ref centroidPeakWidth,    // centroidingPeakWidth
                                                    ref rawData,        // daw data
                                                    ref peakFlags,        // peakFlags
                                                    ref arraySize);        // array size
                    //Scan tScan = new Scan();
                    var peaksCount = 0;
                    for (int j = 0; j < arraySize; j++)
                    {
                        var mz = (double)((Array)rawData).GetValue(0, j);
                        var current = (double)((Array)rawData).GetValue(1, j);

                        // skip any mass with the zero intensity
                        if (current == 0) continue;
                        peaksCount++;
                        curScan.Peaks.MzIntPeaks.Add(new DataPoint
                        {
                            Mz = mz,
                            Intensity = current
                        });
                        //curScan.Peaks.SetValue(mz, current);
                    }

                    //for (k = 0; k < pnArraySize; k++)
                    //{
                    //    var mz = (double)((Array)MassData).GetValue(0, k);
                    //    var inten = (double)((Array)MassData).GetValue(1, k);

                    //    // skip any mass with the zero intensity
                    //    if (inten == 0) continue;

                    //    DataPoint dp = new DataPoint
                    //    {
                    //        Mz = mz,
                    //        Intensity = inten
                    //    };
                    //    curScan.Peaks.MzIntPeaks.Add(dp);
                    //    //curScan.Peaks.SetValue(mz, inten);
                    //}
                    curScan.PeaksCount = peaksCount;
                    if (msLevel == 1)
                        msRun.Scan.Add(curScan);
                    else
                    {
                        Scan parentScan = GetParentScan(msRun.Scan, msLevel);
                        if (parentScan.ChildScan == null)
                            parentScan.ChildScan = new List<Scan>();
                        parentScan.ChildScan.Add(curScan);
                    }
                }
                catch(Exception e)
                {
                    logger.Error(e, "Exception processing scan {0}. {1}", curScanNum);
                    LastMessage = string.Format("Exception processing scan {0}. {1}", curScanNum, e.Message);
                    rawFile.Close();
                    return;
                }
            }

            rawFile.Close();

        }

        //private void StoreDataProcessing(List<msRunDataProcessing> dataProcessing, string v1, string v2)
        //{
        //    dataProcessing[0].processingOperation.Add(new processingOperation { name = v1, value = v2 });
        //}

        private Scan GetParentScan(List<Scan> scan, int msLevel)
        {
            Scan sc = null;
            if (msLevel == 2)
            {
                sc = scan.Last(s => s.MsLevel == "1");
            }
            if (msLevel == 3)
            {
                sc = scan.Last(s => s.MsLevel == "1").ChildScan.Last(s => s.MsLevel == "2");
            }
            return sc;
        }

        private scanScanType GetScanType(IXRawfile5 rawFile, int curScanNum)
        {
            int nType = -1;
            rawFile.GetScanTypeForScanNum(curScanNum, ref nType);
            switch(nType)
            {
                case 0:
                    return scanScanType.Full;
                case 1:
                    return scanScanType.SIM;
                case 2:
                    return scanScanType.zoom;
                case 3:
                    return scanScanType.SRM;
            }
            return scanScanType.Unknown;
        }

        private scanPolarity GetPolarity(string scanFilter, Logger logger)
        {
            
            if (scanFilter == null)
            {
                logger.Error("ERROR: Could not extract scan filter line");
                LastMessage = "ERROR: Could not extract scan filter line";
                //rawFile.Close();
                return scanPolarity.Unknown;
            }
            LastMessage = "OK";
            var pol = ParseFilter(scanFilter, 1);
            if (pol.Equals("-"))
            {
                return scanPolarity.Negative;
            }
            else if (pol.Equals("+"))
            {
                return scanPolarity.Positive;
            }
            else return scanPolarity.Unknown;
        }

        private void GetMsRunMetaInfo(msRun msRun, IXRawfile5 rawFile, int scanNum)
        {
            
            msRun.MsInstrument.msManufacturer.category = "msManufacturer";
            msRun.MsInstrument.msManufacturer.value = "Bruker Daltonics";
            rawFile.SetCurrentController(0, 1);
            msRun.MsInstrument.msModel = GetModel(rawFile);
            msRun.MsInstrument.msMassAnalyzer = GetAnalyzer(rawFile, scanNum);
            //.category = "";
            msRun.MsInstrument.msDetector = GetDetector(rawFile, scanNum, "msDetector");
            int pnControllerType = -1;
            rawFile.GetControllerType(0, ref pnControllerType);
            int numExtra = -1;
            rawFile.GetNumTrailerExtra(ref numExtra);
            rawFile.GetControllerType(1, ref pnControllerType);
            msRun.MsInstrument.msIonisation = GetIonization(rawFile, scanNum);
            msRun.MsInstrument.software = GetSoftware(rawFile);
            msRun.MsInstrument.@operator = GetOperator(rawFile);
            

            msRun.DataProcessing.Add(GetDataProcessing(rawFile));
            rawFile.GetTrailerExtraLabelsForScanNum(scanNum, ref pvarLabels, ref pnArraySize);

            rawFile.GetTrailerExtraLabelsForScanNum(scanNum + 1, ref pvarLabels, ref pnArraySize);
            for (var i = 0; i < pnArraySize; i++)
            {
                object pvarValue = null;
                rawFile.GetTrailerExtraValueForScanNum(scanNum, ((Array)pvarLabels).GetValue(i).ToString(), ref pvarValue);

            }

            //string instrDescr = null;
            //string instrName = null;
            //string instrModel = null;
            //numExtra = 0;
            //rawFile.GetNumberOfControllers(ref numExtra);
            //rawFile.GetNumInstMethods(ref numExtra);
            //rawFile.GetInstrumentDescription(ref instrDescr);
            //rawFile.GetInstMethodNames(ref numExtra, ref pvarLabels);
            //rawFile.GetInstName(ref instrName);
            //rawFile.GetInstModel(ref instrModel);


        }

        /// <remarks>needs to be refactored</remarks>
        private @operator GetOperator(IXRawfile5 rawFile)
        {
            string operatorLastName = null;
            rawFile.GetOperator(ref operatorLastName);
            return new @operator() {
                last = operatorLastName,
                first = string.Empty
            };
        }

        /// <remarks>needs to be refactored</remarks>
        private Software GetSoftware(IXRawfile5 rawFile)
        {
            string instSoftwareVersion = null;
            rawFile.GetInstSoftwareVersion(ref instSoftwareVersion);
            return new Software() { Type = softwareType.acquisition, Name = "XCalibur", Version = instSoftwareVersion };
        }

        private List<processingOperation> GetTuneData(IXRawfile5 rawFile)
        {
            List<processingOperation> retList = new List<processingOperation>();
            int nSegment = 1; // first tune record
            object varLabels = null;
            //VariantInit(&varLabels);
            object varValues = null;
            //VariantInit(&varValues);
            int nArraySize = 0;
            int numTuneData = 0;
            rawFile.GetNumTuneData(ref numTuneData);
            rawFile.GetTuneData(0, ref pvarLabels, ref varValues, ref nArraySize);
            for (var i = 0; i < nArraySize; i++)
            {
                var n = ((Array)pvarLabels).GetValue(i).ToString();
                var v = ((Array)varValues).GetValue(i).ToString();
                if (string.IsNullOrWhiteSpace(n)) continue;
                retList.Add(new processingOperation {
                    name = n,
                    value = v });
            }
            return retList;
        }

        public void GetScanAdditionalParams(Scan curScan, int scanNum)
        {
            foreach (var param in GetScanParams(scanNum))
            {
                var scanAddInfo = new namevalueType()
                {
                    name = param.Key,
                    value = param.Value.ToString()
                };
                curScan.AdditionalParameters.Add(scanAddInfo);
            }
        }
        public Dictionary<string, double> GetScanParams(int scanNum)
        {
            Dictionary<string, double> returnDict = new Dictionary<string, double>();
            //void GetStatusLogForScanNum(int nScanNumber, ref double pdStatusLogRT, ref object pvarLabels, ref object pvarValues, ref int pnArraySize);
            double pdStatusLogRT = double.NaN;
            object pvarLabels = null;
            object pvarValues = null;
            int pnArraySize = -1;
            rawFile.GetStatusLogForScanNum(scanNum, ref pdStatusLogRT, ref pvarLabels, ref pvarValues, ref pnArraySize);

            string[] labelArray = pvarLabels as string[];
            string[] valueArray = pvarValues as string[];
            for (int i = 0; i < labelArray.Length; i++)
            {
                double n;
                if (!returnDict.ContainsKey(labelArray[i]) && double.TryParse(valueArray[i], out n))
                {
                    returnDict.Add(labelArray[i], n);
                }
            }

            return returnDict;
        }
        private ontologyEntryType GetModel(IXRawfile5 rawFile)
        {
            string model = null;
            rawFile.GetInstModel(ref model);
            var retVal = new ontologyEntryType() { category = "msModel", value = model };
            return retVal;
        }

        private msRunDataProcessing GetDataProcessing(IXRawfile5 rawFile)
        {
            string instSoftwareVersion = null;
            rawFile.GetInstSoftwareVersion(ref instSoftwareVersion);
            string instHardwareVersion = null;
            rawFile.GetInstHardwareVersion(ref instHardwareVersion);
            string comm2 = null;
            rawFile.GetOperator(ref comm2);
            //rawFile.getin

            object varFilters = null;
            string filt = null;
            int nArraySize = 0;
            rawFile.GetFilters(ref varFilters, ref nArraySize);
            rawFile.GetFilterForScanNum(1, ref filt);
            var dp = new msRunDataProcessing
            {
                comment = new string[5],
                software = new Software() { Name = "Analyze Scan", Version = CoreAssembly.Version.ToString(), Type = softwareType.processing },
                processingOperation = new List<processingOperation>()
            };
            dp.processingOperation.AddRange(GetTuneData(rawFile));
            for (int i = 0; i < 5; i++)
            {
                string comm = null;
                rawFile.GetSeqRowUserTextEx(i, ref comm);
                if (string.IsNullOrWhiteSpace(comm)) continue;
                dp.comment.SetValue(comm, i);
                //rawFile.GetSeqRowUserTextEx()
            }

            return dp;
        }

        private msRunMsInstrumentMsMassAnalyzer GetAnalyzer(IXRawfile5 rawFile, int scanNum)
        {
            var retVal = new msRunMsInstrumentMsMassAnalyzer() { category = "msMassAnalyzer" };
            int mzanalyzer = -1;
            rawFile.GetMassAnalyzerTypeForScanNum(scanNum, ref mzanalyzer);
            switch ((ThermoMzAnalyzer)mzanalyzer)
            {
                case ThermoMzAnalyzer.FTMS:
                    retVal.value = "Orbitrap (FTMS)";
                    break;
                case ThermoMzAnalyzer.ITMS:
                    retVal.value = "IonTrap2D (ITMS)";
                    break;
                case ThermoMzAnalyzer.Sector:
                    retVal.value = "Sector";
                    break;
                case ThermoMzAnalyzer.TOFMS:
                    retVal.value = "TOF (TOFMS)";
                    break;
                default:
                    retVal.value = "Uknonwn";
                    break;
            }
            return retVal;
        }

        private ontologyEntryType GetIonization(IXRawfile5 rawFile, int scanNum)
        {
            string scanFilter = null;
            rawFile.GetFilterForScanNum(scanNum, ref scanFilter);

            //MS_IonizationMode mode = ParseIonizationType(ParseFilter(scanFilter, 3));
            ontologyEntryType type = new ontologyEntryType() { category = "msIonisation", value = ParseFilter(scanFilter, 3) };
            return type;
        }

        private string ParseFilter(string scanFilter, int v)
        {
            var parts = scanFilter.Split(new[] { ' ' });
            return parts[v];
        }

        private MS_IonizationMode ParseIonizationType(string word)
        {
            if (word == "EI")
                return MS_IonizationMode.MS_ElectronImpact;
            else if (word == "CI")
                return MS_IonizationMode.MS_ChemicalIonization;
            else if (word == "FAB")
                return MS_IonizationMode.MS_FastAtomBombardment;
            else if (word == "ESI")
                return MS_IonizationMode.MS_Electrospray;
            else if (word == "APCI")
                return MS_IonizationMode.MS_AtmosphericPressureChemicalIonization;
            else if (word == "NSI")
                return MS_IonizationMode.MS_Nanospray;
            else if (word == "TSP")
                return MS_IonizationMode.MS_Thermospray;
            else if (word == "FD")
                return MS_IonizationMode.MS_FieldDesorption;
            else if (word == "MALDI")
                return MS_IonizationMode.MS_MatrixAssistedLaserDesorptionIonization;
            else if (word == "GD")
                return MS_IonizationMode.MS_GlowDischarge;
            else
                return MS_IonizationMode.MS_AcceptAnyIonizationMode;
        }
    private ontologyEntryType GetActivationType(IXRawfile5 rawFile, int scanNum, int msnOrder = 2)
        {
            int type = -1;
            var retVal = new ontologyEntryType() { category = "msIonisation" };
            rawFile.GetActivationTypeForScanNum(scanNum, msnOrder, ref type);
            switch ((DissociationType)type)
            {
                case DissociationType.CI:
                    retVal.value = "CI";
                    break;
                case DissociationType.CID:
                    retVal.value = "CID";
                    break;
                case DissociationType.ECD:
                    retVal.value = "ECD";
                    break;
                case DissociationType.ETD:
                    retVal.value = "ETD";
                    break;
                case DissociationType.HCD:
                    retVal.value = "HCD";
                    break;
                case DissociationType.MPD:
                    retVal.value = "MPD";
                    break;
                case DissociationType.NETD:
                    retVal.value = "NETD";
                    break;
                case DissociationType.None:
                    retVal.value = "None";
                    break;
                case DissociationType.NPTR:
                    retVal.value = "NPTR";
                    break;
                case DissociationType.PQD:
                    retVal.value = "PQD";
                    break;
                case DissociationType.PTR:
                    retVal.value = "PTR";
                    break;
                case DissociationType.SA:
                    retVal.value = "SA";
                    break;
                default:
                    retVal.value = "Uknonwn";
                    break;
            }
            return retVal;
        }

        private MsDetector GetDetector(IXRawfile5 rawFile, int scanNum, string category)
        {
            int pnDetectorType = -1;
            var retVal = new MsDetector() { category = category };
            rawFile.GetDetectorTypeForScanNum(scanNum, ref pnDetectorType);
            switch (pnDetectorType)
            {
                case 0:
                    retVal.value = "CID";
                    break;
                case 1:
                    retVal.value = "PQD";
                    break;
                case 2:
                    retVal.value = "ETD";
                    break;
                case 3:
                    retVal.value = "HCD";
                    break;
                default:
                    retVal.value = "Uknonwn";
                    break;
            }
            return retVal;
        }

        private void GetScanHeaderInformation(Scan curScan, int scanNum)
        {
            numDataPoints = 0;
            minObservedMZ = 0;
            maxObservedMZ = 0;
            totalIonCurrent = 0;
            basePeakMZ = 0;
            basePeakIntensity = 0;
            rawFile.GetScanHeaderInfoForScanNum(scanNum,
                                                    ref numDataPoints,
                                                    ref retentionTimeInMinutes,
                                                    ref minObservedMZ,
                                                    ref maxObservedMZ,
                                                    ref totalIonCurrent,
                                                    ref basePeakMZ,
                                                    ref basePeakIntensity,
                                                    ref channel, // unused
                                                    ref uniformTime, // unused
                                                    ref frequency // unused
                                                    );
            //rawFile.
            curScan.RetentionTimeTs = TimeSpan.FromMinutes(retentionTimeInMinutes);
            curScan.totIonCurrent = totalIonCurrent;
            curScan.basePeakIntensity = basePeakIntensity;
            curScan.basePeakMz = basePeakMZ;
            curScan.highMz = maxObservedMZ;
            curScan.lowMz = minObservedMZ;
            curScan.PeaksCount = numDataPoints;
            curScan.MsLevel = msLevel.ToString();
            curScan.Num = scanNum;
        }

        public Raw()
        {
            IXRawfile5 rawFile = (IXRawfile5)new MSFileReader_XRawfile();
        }
    }
}
