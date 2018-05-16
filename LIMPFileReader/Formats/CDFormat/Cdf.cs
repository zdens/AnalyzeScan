using System;
using System.IO;
using LIMPFileReader.Formats.Mzxml;
using CDF = Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Factory;
using Microsoft.Research.Science.Data.Imperative;
using System.Collections.Generic;
using NLog;
using System.Linq;
//using net;

namespace LIMPFileReader.Formats.CDFormat
{
    public class Cdf : LimpXml
    {
        public Cdf(string scanPath, Logger logger)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            DataSetFactory.SearchFolder(Environment.CurrentDirectory);
            using (FileStream fs = new FileStream(scanPath, FileMode.Open))
            {
                msRun.parentFile.Add(new msRunParentFile { fileName = scanPath, fileType = Mzxml.msRunParentFileFileType.RAWData, fileSha1 = ComputeSha1(fs) });
            }
        
            var cdf = CDF.DataSet.Open(scanPath);

            msRun.MsInstrument = GetInstrument(cdf);
            //var intensities = cdf.GetData<double[]>("intensity_values");
            //var masses = cdf.GetData<double[]>("mass_values");
            var polarity = scanPolarity.Unknown;
            if (cdf.Metadata["test_ionization_polarity"].ToString().ToLower().Contains("negative"))
                polarity = scanPolarity.Negative;
            if (cdf.Metadata["test_ionization_polarity"].ToString().ToLower().Contains("positive"))
                polarity = scanPolarity.Positive;
            msRun.StartTime = cdf.Metadata["experiment_date_time_stamp"].ToString().Trim();
            
            
            
            var scans = cdf.GetData<int[]>("scan_index");
            double[] retentionTimes = new double[scans.Length];
            var scanTimes = cdf.GetData<double[]>("scan_acquisition_time");

            msRun.Scan.AddRange(GetScans(cdf, polarity, logger));

            msRun.Scan[0].AdditionalParameters.AddRange(GetMetadata(cdf.Metadata));
            msRun.DataProcessing.Add(GetDataProcessing(cdf.Metadata));
            //for (var i = 0; i < scans.Length; i++)
            //{
            //    Scan curScan = new Scan();
            //    curScan.Polarity = polarity;
            //    curScan.Num = i.ToString();
            //    curScan.totIonCurrent = scans[i];
            //    curScan.retentionTime = (scanTimes[i] / 60).ToString();     // convert from minutes to seconds
            //    msRun.Scan.Add(curScan);
            //}

            //foreach (var cdfScan in GetData(cdf.GetData<double[]>()))

        }

        private msRunDataProcessing GetDataProcessing(CDF.MetadataDictionary metadata)
        {
            return new msRunDataProcessing
            {
                comment = new string[] {
                    metadata["sample_prep_comments"].ToString().Trim(),
                    metadata["sample_comments"].ToString().Trim(),
                    string.Format("Experiment title: {0}", metadata["experiment_title"].ToString().Trim())
                },
                software = new Software() { Name = "Analyze Scan", Version = CoreAssembly.Version.ToString()}
            };
        }

        private IEnumerable<namevalueType> GetMetadata(CDF.MetadataDictionary metadata)
        {
            var list = new List<namevalueType>();
            foreach (var md in metadata)
            {
                if (md.Key.Equals("Name"))
                    continue;
                var val = md.Value.ToString().Trim();
                msRun.Scan[0].AdditionalParameters.Add(new namevalueType { name = md.Key, value = val });
            }
            return list;
        }

        private IEnumerable<Scan> GetScans(CDF.DataSet cdf, scanPolarity scanPolarity, Logger logger)
        {
            logger = LogManager.GetLogger("Variables");
            var masses = cdf.GetData<double[]>("mass_values");
            var scans = cdf.GetData<int[]>("scan_index");
            var attr = cdf.GetAttr(0, "test_separation_type");
            double[] retentionTimes = new double[scans.Length];
            int[] scanStartPositions = new int[scans.Length + 1];

            var scanTimes = cdf.GetData<double[]>("scan_acquisition_time");
            CDF.Variable massvar = cdf.Variables.All.First(v => v.Name.Equals("mass_values"));
            CDF.Variable intensities = cdf.Variables.All.First(v => v.Name.Equals("intensity_values"));
            double scaleFactorMass = (double)massvar.Metadata["scale_factor"];
            double scaleFactorInt = (double)intensities.Metadata["scale_factor"];

            var msScans = new List<Scan>();
            var numberOfGoodScans = 0;
            index.name = "scan";
            index.offset = new List<mzXMLIndexOffset>();
            for (var i = 0; i < scans.Length; i++)
            {
                Scan curScan = new Scan
                {
                    Polarity = scanPolarity,
                    Num = i
                };
                scanStartPositions[i] = scans[i];
                curScan.retentionTime = (scanTimes[i] / 60).ToString();     // convert from minutes to seconds
                msScans.Add(curScan);
                if (scanStartPositions[i] >= 0)
                {
                    numberOfGoodScans++;
                }
            }
            scanStartPositions[scans.Length] = masses.Length;
            var totalScans = scans.Length;
            if (numberOfGoodScans < totalScans)
            {

                // Fix scan_acquisition_time
                // - calculate average delta time between present scans
                double sumDelta = 0;
                int n = 0;
                for (int i = 0; i < totalScans; i++)
                {
                    // Is this a present scan?
                    if (scanStartPositions[i] < 0)
                        continue;
                    // Yes, find next present scan
                    for (int j = i + 1; j < totalScans; j++)
                    {
                        if (scanStartPositions[j] >= 0)
                        {
                            sumDelta += (retentionTimes[j] - retentionTimes[i])
                                / (j - i);
                            n++;
                            break;
                        }
                    }
                }
                double avgDelta = sumDelta / n;
                // - fill missing scan times using nearest good scan and avgDelta
                for (int i = 0; i < totalScans; i++)
                {
                    // Is this a missing scan?
                    if (scanStartPositions[i] >= 0)
                        continue;
                    // Yes, find nearest present scan
                    int nearestI = int.MaxValue;
                    for (int j = 1; 1 < 2; j++)
                    {
                        if ((i + j) < totalScans)
                        {
                            if (scanStartPositions[i + j] >= 0)
                            {
                                nearestI = i + j;
                                break;
                            }
                        }
                        if ((i - j) >= 0)
                        {
                            if (scanStartPositions[i - j] >= 0)
                            {
                                nearestI = i + j;
                                break;
                            }
                        }

                        // Out of bounds?
                        if (((i + j) >= totalScans) && ((i - j) < 0))
                        {
                            break;
                        }
                    }

                    if (nearestI != int.MaxValue)
                    {

                        retentionTimes[i] = retentionTimes[nearestI]
                            + (i - nearestI) * avgDelta;

                    }
                    else
                    {
                        if (i > 0)
                        {
                            retentionTimes[i] = retentionTimes[i - 1];
                        }
                        else
                        {
                            retentionTimes[i] = 0;
                        }
                        //logger.severe("ERROR: Could not fix incorrect QStar scan times.");
                    }
                }

                // Fix scanStartPositions by filling gaps with next good value
                for (int i = 0; i < totalScans; i++)
                {
                    if (scanStartPositions[i] < 0)
                    {
                        for (int j = i + 1; j < (totalScans + 1); j++)
                        {
                            if (scanStartPositions[j] >= 0)
                            {
                                scanStartPositions[i] = scanStartPositions[j];
                                break;
                            }
                        }
                    }
                    msScans[i].retentionTime = retentionTimes[i].ToString();
                }
            }
            Dictionary<int, int[]> scansIndex = new Dictionary<int, int[]>();
            for (int i = 0; i < totalScans; i++)
            {
                int[] startAndLength = new int[2];
                startAndLength[0] = scanStartPositions[i];
                startAndLength[1] = scanStartPositions[i + 1] - scanStartPositions[i];

                //scansRetentionTimes.put(scanNum, new Double(retentionTimes[i]));
                scansIndex.Add(i, startAndLength);

            }
            for (int i = 0; i < totalScans; i++)
            {
                int[] scanStartPosition = new int[1];
                int[] scanLength = new int[1];
                int[] startAndLength = scansIndex[i];
                scanStartPosition[0] = startAndLength[0];
                scanLength[0] = startAndLength[1];
                if (scanLength[0] == 0)
                    continue;
                Array massValueArray = massvar.GetData(scanStartPosition, scanLength);
                Array intensityValueArray = intensities.GetData(scanStartPosition, scanLength);
                double tic = 0;
                for (int j = 0; j < massValueArray.Length; j++)
                {
                    var mz = (double)massValueArray.GetValue(j) * scaleFactorMass;
                    var inten = (int)intensityValueArray.GetValue(j) * scaleFactorInt;
                    tic += inten;
                    msScans[i].Peaks.MzIntPeaks.Add(new DataPoint
                    {
                        Mz = mz,
                        Intensity = inten
                    });
                    msScans[i].Peaks.SetValue(mz, inten);
                }
                msScans[i].PeaksCount = msScans[i].Peaks.MzIntPeaks.Count;
                msScans[i].totIonCurrent = tic;
                msScans[i].FindBasePeak();
                msScans[i].FindLowHighMz();
                index.offset.Add(new mzXMLIndexOffset { id = msScans[i].Num });
            }
            return msScans;
        }

        private msRunMsInstrument GetInstrument(CDF.DataSet dataset)
        {
            var instrument = new msRunMsInstrument();

            instrument.msManufacturer.value = GetString(dataset.GetData<sbyte[,]>("instrument_mfr"));
            instrument.msMassAnalyzer.value = GetString(dataset.GetData<sbyte[,]>("instrument_name"));
            instrument.software.Version = GetString(dataset.GetData<sbyte[,]>("instrument_sw_version"));
            instrument.msIonisation.value = dataset.Metadata["test_ionization_mode"].ToString();
            instrument.msDetector.value = dataset.Metadata["test_detector_type"].ToString();
            instrument.msModel.value = GetString(dataset.GetData<sbyte[,]>("instrument_model"));
            instrument.@operator.Value = dataset.Metadata["operator_name"].ToString();
            return instrument;
        }

        private string GetString(sbyte[,] sbv)
        {
            string val = string.Empty;
            foreach (sbyte v in sbv)
            {
                val += Char.ConvertFromUtf32(v).ToString();
            }
            return val;
        }
    }
}
