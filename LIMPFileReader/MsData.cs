using LIMPFileReader.Helpers;
using System;
using System.IO;
using System.Linq;
using NLog;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace LIMPFileReader
{
    public class MsData
    {
        private string fp;
        private Logger logger;
        private StringCollection reportData;

        public string ScanPath { get; set; }
        public string ScanType { get; set; }
        public object ScanData { get; set; }
        public string StatusMessage { get; set; }
        public StringCollection ReportParams { get; set; }

        public MsData(string filePath, Logger logger, StringCollection reportParams)
        {
            object data = null;
            ReportParams = reportParams;
            if (File.Exists(filePath))
            {
                ScanPath = filePath;
                ScanType = OpenFile(logger);
            }
            else if (Directory.Exists(filePath))
            {
                ScanPath = filePath;
                ScanType = OpenDirectory(logger);
            }
            else
            {
                StatusMessage = "File is not found";
                logger.Error(StatusMessage);
                return;
            }
                //throw new FileNotFoundException();
            data = ScanData;

            //switch (ScanType)
            //{
            //    case "mzxml":
            //        data = (Formats.Mzxml.mzXML)ScanData;
            //        break;
            //    case "raw":
            //        data = (Formats.Thermo.Raw)ScanData;
            //        break;
            //    case "bruker":
            //        data = (Formats.Bruker.MsXCompass)ScanData;
            //        break;
            //}
        }

        /// <summary>
        /// Get data from device
        /// </summary>
        public MsData()
        {

        }

       private string OpenDirectory(Logger logger)
        {
            string result = "unknown";
            if (File.Exists(Path.Combine(ScanPath, "Analysis.yep")))
            {
                ScanData = new Formats.Bruker.MsXCompass(ScanPath, logger, ReportParams);
                return "bruker";
            }
            return result;
        }

        public string OpenFile(Logger logger)
        {
            string result = "unknown";

            if (ScanPath.EndsWith("Analysis.yep"))
            {
                ScanPath = Path.GetDirectoryName(ScanPath);
                result = OpenDirectory(logger);
                return result;
            }

            int bytesToRead = 2048;
            char[] buffer = new char[bytesToRead];
            
            try
            {
                var sr = new StreamReader(ScanPath);
                var readedBytes = sr.ReadBlock(buffer, 0, bytesToRead);
                sr.Close();
                if (readedBytes < bytesToRead)
                    throw new Exception("The file is corrupted");
                var readedStrng = string.Join("", buffer.Select(c => c.ToString()).ToArray());
                if (readedStrng.Contains("netcdf_revision"))
                {
                    ScanData = new Formats.CDFormat.Cdf(ScanPath, logger);
                    return "cdf";
                }
                // TODO: разобраться с файлами от Bruker
                if (readedStrng.Contains("Yellop"))
                {
                    try
                    {
                        //ScanData = new Formats.Bruker.MsXCompass(ScanPath);
                        return "yep";
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error while trying to process yep", e);
                    }
                    
                }
                if (readedStrng.Contains("mzXML"))
                {
                    try
                    {
                        ScanData = new LimpXml(ScanPath);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error while trying to read mzXml", e);
                    }
                    return "mxzml";
                }
                if (CheckXcalibur())
                {
                    try
                    {
                        ScanData = new Formats.Thermo.Raw(ScanPath, logger);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Error while trying to read raw", e);
                    }
                    return "raw";
                }
                //return CheckXcalibur();
            }
            catch (Exception e)
            {
                //throw new Exception("Cannot process the file specified", e);
                logger.Error(e, "Cannot process the file specified");
                StatusMessage = string.Format("{0};{1}", "Cannot process the file specified", e.Message);
                result = string.Empty;
            }
            return result;
        }

        private bool CheckXcalibur()
        {
            var fExt = Path.GetExtension(ScanPath);
            if (fExt.EndsWith(".raw"))
            {
                return true;
            }
            return false;
        }
    }
}
