using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using EDAL;
using LIMPFileReader.Formats.Mzxml;
using NLog;
using static LIMPFileReader.Formats.Mzxml.MsDetector;

namespace LIMPFileReader.Formats.Bruker
{
    [XmlRoot("mzXML", Namespace = "http://sashimi.sourceforge.net/schema_revision/mzXML_2.1", IsNullable = false)]
    public class MsXCompass : LimpXml
    {
        //private string scanPath;
        //private EDAL.MSAnalysis analysis;
        //public EDAL.MSAnalysis Analysis { get { return analysis; } }

        EDAL.IMSAnalysis2 analysis;
        [XmlIgnore()]
        public string LastMessage { get; set; }

        //public Mzxml.mzXML mzFormat { get; set; }

        public MsXCompass()
        {
            analysis = new MSAnalysis();
        }

        public MsXCompass(string scanPath, Logger logger, StringCollection reportParams)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            MSAnalysis analysisold = new MSAnalysis();
            analysis = (IMSAnalysis2)analysisold;
            try
            {
                analysis.Open(scanPath);

                msRun.parentFile.Add(GetParentFile(scanPath));

                msRun.StartTime = analysis.AnalysisDateTime.ToString(new CultureInfo("en-us"));
                msRun.MsInstrument.msManufacturer = GetManufactor(analysis);
                msRun.MsInstrument.msModel = GetModel(analysis);
                msRun.MsInstrument.msMassAnalyzer = GetAnalyzer(analysis);
                msRun.MsInstrument.msDetector = GetDetector(analysis);
                msRun.MsInstrument.msIonisation = GetIonization();
                msRun.MsInstrument.@operator = GetOperator();
                msRun.MsInstrument.software = GetSoftware();
                msRun.DataProcessing.Add(GetDataProcessing(analysis));

                if (!string.IsNullOrWhiteSpace(analysis.AnalysisDescription))
                {
                    msRun.DataProcessing[0].comment = new string[] { string.Format("Descritpion: {0}", analysis.AnalysisDescription) };
                }
                var scanIndex = new mzXMLIndex();
                scanIndex.offset = new List<mzXMLIndexOffset>();
                msRun.Scan = GetScans(logger, reportParams, ref scanIndex);
                index = scanIndex;

            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                //COM error
                throw new Exception("There were problems during dumping the data of analysis " + scanPath, e);
            }
            catch (SystemException e)
            {
                //any other error, e.g. can't find the DLL
                throw new Exception("There were problems during dumping the data of analysis " + scanPath, e);
            }
        }

        /// <remarks>We believe scanPath is directory</remarks>
        private msRunParentFile GetParentFile(string scanPath)
        {
            //var fp = Path.Combine(scanPath, "Analysis.yep");
            var files = Directory.GetFiles(scanPath, "*.yep");
            var f = new msRunParentFile();
            if (files.Any())
            {
                f.fileName = files[0];
                f.fileType = msRunParentFileFileType.RAWData;
                f.fileSha1 = GetSha1(files[0]);
            }
            return f;
        }

        private string GetSha1(string v)
        {
            var hash = string.Empty;
            using (FileStream fs = new FileStream(v, FileMode.Open))
            using (var cryptoProvider = new SHA1CryptoServiceProvider())
            {
                hash = BitConverter.ToString(cryptoProvider.ComputeHash(fs)).Replace("-", "").ToLower();
            }
            return hash;
            //using (BufferedStream bs = new BufferedStream(fs))
            //{
            //    using (SHA1Managed sha1 = new SHA1Managed())
            //    {
            //        byte[] hash = sha1.ComputeHash(bs);
            //        StringBuilder formatted = new StringBuilder(2 * hash.Length);
            //        foreach (byte b in hash)
            //        {
            //            formatted.AppendFormat("{0:X2}", b);
            //        }
            //    }
            //}
        }

        /// <remarks>needs to be refactored</remarks>
        private Software GetSoftware()
        {
            return new Software() { Type = softwareType.acquisition, Name = analysis.InstrumentDescription };
        }

        private @operator GetOperator()
        {
            return new @operator { first = string.Empty, last = analysis.OperatorName };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns></returns>
        private msRunMsInstrumentMsMassAnalyzer GetAnalyzer(IMSAnalysis2 analysis)
        {
            return new msRunMsInstrumentMsMassAnalyzer { category = "msMassAnalyzer", value = analysis.InstrumentFamily.ToString() };
        }

        private ontologyEntryType GetModel(IMSAnalysis2 analysis)
        {
            return new ontologyEntryType { category = "msModel", value = analysis.InstrumentDescription };
        }

        /// <summary>
        /// CompasXtract Interface have not got methods for getting this value from data-file
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns>new msRunMsInstrumentMsManufacturer</returns>
        /// <remarks>needs to be refactored</remarks>
        private msRunMsInstrumentMsManufacturer GetManufactor(IMSAnalysis2 analysis)
        {
            return new msRunMsInstrumentMsManufacturer() { category = "msManufacturer", value = "Bruker Daltonics" };
        }

        /// <summary>
        /// CompasXtract Interface have not got methods for getting this value from data-file
        /// </summary>
        /// <param name="analysis"></param>
        /// <returns>ontologyEntryType</returns>
        /// <remarks>needs to be refactored</remarks>
        private MsDetector GetDetector(IMSAnalysis2 analysis)
        {
            var type = new MsDetector() { category = "msDetector" };
            switch (analysis.InstrumentFamily)
            {
                case InstrumentFamily.InstrumentFamily_Trap:
                    type.value = string.Format("MS:{0}", (int)DetectorType.MS_electron_multiplier);
                    break;
                case InstrumentFamily.InstrumentFamily_MaldiTOF:
                case InstrumentFamily.InstrumentFamily_OTOF:
                    type.value = string.Format("MS:{0}", (int)DetectorType.MS_photomultiplier);
                    break;
                default:
                    type.value = string.Format("MS:{0}", (int)DetectorType.MS_Unknown);
                    break;
            }
            return type;
        }

        private msRunDataProcessing GetDataProcessing(IMSAnalysis2 metadata)
        {
            var dp = new msRunDataProcessing
            {
                comment = new string[]{
                    string.Format("Analysis name: {0}", metadata.AnalysisName),
                    string.Format("Method name: {0}", metadata.MethodName),
                    string.Format("Sample: {0}", metadata.SampleName),
                },
                software = new Software() { Name = "Analyze Scan", Version = CoreAssembly.Version.ToString(), Type = softwareType.processing },
                processingOperation = new List<processingOperation>()
            };
            dp.processingOperation.AddRange(GetProcessingOperation(metadata));
            return dp;
        }

        private List<processingOperation> GetProcessingOperation(IMSAnalysis2 metadata)
        {
            var lst = new List<processingOperation>();
            lst.Add(new processingOperation() { value = metadata.MethodName, name = "Method name" });
            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="index"></param>
        /// <remarks>needs to be refactored</remarks>
        /// <remarks>Set Centroided = 0</remarks>
        /// <returns></returns>
        private List<Scan> GetScans(Logger logger, StringCollection reportParams, ref mzXMLIndex index)
        {
            object masses, intensities;
            List<Scan> retList = new List<Scan>();
            Array intensitiesLine, massesLine;
            MSSpectrumCollection spectra = analysis.MSSpectrumCollection;
            
            msRun.scanCount = spectra.Count.ToString();
            logger.Info("Scans count: ", spectra.Count);
            index.name = "scan";
            msRun.DataProcessing[0].StoreDataProcessing("Count of Scans", spectra.Count.ToString());
            for (int specID = 1; specID <= spectra.Count; ++specID)
            {
                try
                {
                    // TODO: разобраться с профилями
                    index.offset.Add(new mzXMLIndexOffset { id = specID });
                    IMSSpectrum2 spectrum2 = (IMSSpectrum2)spectra[specID];
                    if (spectrum2 != null)
                    {
                        //spectrum2.GetMassIntensityValues(SpectrumTypes.SpectrumType_Line, out masses, out intensities);
                        spectrum2.GetMassIntensityValues(SpectrumTypes.SpectrumType_Profile, out masses, out intensities);
                        intensitiesLine = (Array)intensities;
                        massesLine = (Array)masses;
                    }
                    else // cast was not successful                
                    {
                        // same as above but we don't know if we will get both Line or Profile Spectra
                        spectra[specID].GetMassIntensityValues(SpectrumTypes.SpectrumType_Profile, out masses, out intensities);
                        intensitiesLine = (Array)intensities;
                        massesLine = (Array)masses;

                        //spectra[specID].GetMassIntensityValues(SpectrumTypes.SpectrumType_Profile, out masses, out intensities);
                    }
                    if (intensitiesLine == null)
                        continue;

                    Scan scan = new Scan
                    {
                        RetentionTimeTs = TimeSpan.FromSeconds(spectra[specID].RetentionTime),
                        Polarity = GetPolarity(spectra[specID]),
                        //PeaksCount = intensitiesLine.Length,
                        Centroided = 0,
                        MsLevel = spectra[specID].MSMSStage.ToString(),
                        Peaks = new Peaks()
                        {
                            Precision = scanPeaksPrecision.Item64,
                            PairOrder = "m/z-int",
                            ByteOrder = "network"
                        },
                        Num = specID
                    };
                    
                    if (specID == 1)
                    {
                        LastMessage = GetScanParams(spectra[specID], logger, reportParams, msRun.DataProcessing[0], ref scan);
                    }
                    //if (double.TryParse(scan.GetAdditionalParams("Scan Begin").ToString(), out double dresult))
                    //{
                    //    scan.lowMz = dresult;
                    //}
                    //if (double.TryParse(scan.GetAdditionalParams("Scan End").ToString(), out dresult))
                    //{
                    //    scan.highMz = dresult;
                    //}


                    var bList = new List<byte>();
                    //scan.Peaks.Value = new byte[intensitiesLine.Length];
                    var peaksCount = 0;
                    for (int i = 0; i < intensitiesLine.Length; i++)
                    {
                        var current = (double)intensitiesLine.GetValue(i);
                        if (current == 0) continue;
                        peaksCount++;
                        var mz = (double)massesLine.GetValue(i);
                        //scan.Peaks.SetValue(mz, current);

                        if (scan.basePeakMz < current)
                            scan.basePeakMz = current;
                        scan.totIonCurrent += current;
                        scan.Peaks.MzIntPeaks.Add(new DataPoint
                        {
                            Mz = mz,
                            Intensity = current
                        });
                    }
                    if (peaksCount > 0)
                    {
                        scan.FindBasePeak();
                        scan.FindLowHighMz();
                    }
                    scan.PeaksCount = peaksCount;
                    //scan.Peaks.Value = bList.ToArray();
                    logger.Info("Scan {0}: totIonCurrent = {1}, retentionTime = {2}", scan.Num, scan.totIonCurrent, scan.retentionTime);
                    retList.Add(scan);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Exception occurred at scan {0}", specID);
                }
            }
            return retList;
        }

        

        private string GetScanParams(IMSSpectrum iMSSpectrum, Logger logger, StringCollection reportParams, msRunDataProcessing dp, ref Scan sc)
        {
            foreach (var param in iMSSpectrum.MSSpectrumParameterCollection)
            {
                IMSSpectrumParameter t = ((IMSSpectrumParameter)param);
                try
                {
                    namevalueType scanParam = new namevalueType
                    {
                        name = t.ParameterName,
                        type = t.GroupName,
                        value = t.ParameterValue.ToString()
                    };
                    var rpList = reportParams.Cast<string>();
                    if (rpList.Contains(t.ParameterName, new ParametersComparer()))
                    {
                        //dp.processingOperation.Add(new processingOperation { name = t.ParameterName, type = t.GroupName, value = t.ParameterValue + " (" + t.ParameterUnit + ")" });
                        dp.StoreDataProcessing(t.ParameterName, t.ParameterValue + 
                            (string.IsNullOrWhiteSpace(t.ParameterUnit) ? string.Empty : " (" + t.ParameterUnit + ")"), t.GroupName);
                    }
                    //scan.AdditionalParameters.Add(scanParam);
                    sc.AdditionalParameters.Add(scanParam);
                    logger.Info("Scan param: {0} = {1}", scanParam.name, scanParam.value);
                }
                catch (Exception e)
                {
                    LastMessage = string.Format("Exception while processing param {0}, value = {1}. Exception: {2}", t.ParameterName, t.ParameterValue, e.Message);
                    logger.Error(e, "Exception while processing param {0}, value = {1}.", t.ParameterName, t.ParameterValue);
                    return LastMessage;
                }
            }
            return "OK";
        }

        private scanPolarity GetPolarity(IMSSpectrum iMSSpectrum)
        {
            switch (iMSSpectrum.Polarity)
            {
                case SpectrumPolarity.IonPolarity_Negative:
                    return scanPolarity.Negative;
                case SpectrumPolarity.IonPolarity_Positive:
                    return scanPolarity.Positive;
                default:
                    return scanPolarity.Unknown;
            }
        }

        private string GetScanParam(IMSSpectrum iMSSpectrum, string paramName)
        {
            var paramCollection = iMSSpectrum.MSSpectrumParameterCollection;
            //var s = string.Format("{0}", paramCollection["Ion Source Type"].ParameterValue);
            return string.Format("{0}", paramCollection[paramName].ParameterValue);
        }

        private ontologyEntryType GetIonization()
        {
            var retParam = new ontologyEntryType();
            retParam.category = "msIonisation";
            EDAL.MSSpectrumCollection spectra = analysis.MSSpectrumCollection;
            retParam.value = GetScanParam(spectra[1], "Ion Source Type");
            return retParam;
        }

        public void Serialize(string fileName)
        {
            string res = "OK";
            try
            {
                XmlSerializer ser = new XmlSerializer(this.GetType());

                TextWriter writer = new StreamWriter(fileName);
                ser.Serialize(writer, this);
                writer.Close();

            }
            catch (Exception e)
            {
                res = e.Message;
            }
        }
    }
}
