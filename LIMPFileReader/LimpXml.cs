using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LIMPFileReader.Formats.Mzxml;
using LIMPFileReader.Helpers;

namespace LIMPFileReader
{
    public class LimpXml : mzXML
    {
        [XmlIgnore()]
        public List<Data> RawScan { get; set; }

        public LimpXml()
        {
            RawScan = new List<Data>();
            index = new mzXMLIndex();
            msRun = new msRun();

            //foreach (var scan in msRun.Scan)
            //{
            //    RawScan.Add(new Data() { RetentionTime = ParseTime(scan.retentionTime), TotalIonCurrent = scan.totIonCurrent });
            //}
        }

        public LimpXml(string filePath)
        {
            index = new mzXMLIndex();
            msRun = new msRun();
            RawScan = new List<Data>();

            StreamReader srReader = new StreamReader(filePath);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(mzXML));
            var mzxml = (mzXML)xmlSerializer.Deserialize(srReader);
            srReader.Close();

            index = mzxml.index;
            msRun = mzxml.msRun;
            msRun.parentFile.Add(new msRunParentFile() { fileName = filePath });
            indexOffset = mzxml.indexOffset;
            sha1 = mzxml.sha1;
            //msRun.Scan.Clear();
            //foreach (var fscan in mzxml.msRun.Scan)
            //{
            //    Scan scan = new Scan
            //    {
            //        retentionTime = fscan.retentionTime,
            //        Polarity = fscan.Polarity,
            //        PeaksCount = fscan.PeaksCount,
            //        Centroided = fscan.Centroided,
            //        MsLevel =fscan.MsLevel,
            //        basePeakIntensity = fscan.basePeakIntensity,
            //        basePeakMz = fscan.basePeakMz,
            //        totIonCurrent = fscan.totIonCurrent,
            //        Peaks = new Peaks()
            //        {
            //            Precision = scanPeaksPrecision.Item64,
            //            PairOrder = "m/z-int",
            //            ByteOrder = "network",
            //            Value = fscan.Peaks.Value
            //        },
            //        Num = fscan.Num
            //    };
            //    msRun.Scan.Add(scan);
            //    //RawScan.Add(new Data() { RetentionTime = ParseTime(scan.retentionTime), TotalIonCurrent = scan.totIonCurrent });
            //}
        }

        public static double GetDigit(string s, string patt)
        {
            float res;
            NumberStyles style;
            CultureInfo culture;
            style = NumberStyles.Any;
            culture = CultureInfo.CreateSpecificCulture("en-GB");
            if (float.TryParse(s.Replace(",", ".").Replace(patt, ""), style, culture, out res))
            {
                return res;
            }
            throw new Exception("Cannot convert the string " + s);
        }

        public static TimeSpan ParseTime(string s)
        {
            var regEx = new Regex(@"P(\d+Y)?(\d+M)?(\d+D)?T(\d+H)?(\d+M)?(\d+\.\d+S)?");
            var groups = regEx.Split(s);
            TimeSpan sec = new TimeSpan();
            TimeSpan min = new TimeSpan();
            TimeSpan hour = new TimeSpan();

            bool isXmlFormat = false;

            foreach (var group in groups)
            {

                if (string.IsNullOrEmpty(group))
                    continue;
                if (group.Contains("S"))
                {
                    sec = TimeSpan.FromSeconds(GetDigit(group, "S"));
                    isXmlFormat = true;
                }
                if (group.Contains("M"))
                {
                    min = TimeSpan.FromMinutes(GetDigit(group, "M"));
                    isXmlFormat = true;
                }
                if (group.Contains("H"))
                {
                    hour = TimeSpan.FromHours(GetDigit(group, "H"));
                    isXmlFormat = true;
                }
            }
            if (isXmlFormat)
            {
                return sec + min + hour;
            }
            else
            {
                return TimeSpan.FromSeconds(GetDigit(s, " "));
            }
            //TimeSpan ts = new TimeSpan(hour, min, sec);
        }

        public string ComputeSha1(Stream buffer)
        {
            var sha1 = string.Empty;
            using (var cryptoProvider = new SHA1CryptoServiceProvider())
            {
                sha1 = BitConverter.ToString(cryptoProvider.ComputeHash(buffer));
            }
            return sha1;
        }

        public Spectrum GetSpectrum()
        {
            var spectrum = new Spectrum();

            foreach(var _scan in msRun.Scan)
            {
                foreach(var peaks in _scan.Peaks.MzIntPeaks)
                {
                    spectrum.Masses.Add(peaks.Mz);
                    spectrum.Intensities.Add(peaks.Intensity);
                }
                //var m = _scan.Peaks.
            }

            return spectrum;
        }
    }
}
