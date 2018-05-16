using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMPFileReader;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Xml.Serialization;
using System.IO;
using LIMPFileReader.Formats.Mzxml;
using AnalyzeScan.DataBase;
using AnalyzeScan.Properties;
using LIMPFileReader.Helpers;
using AnalyzeScan.Report;
using NLog;

namespace AnalyzeScan
{
    [Serializable]
    [XmlRoot("export")]
    public class Exports
    {
        public MsPatient Patient { get; set; }
        public MsTissue Tissue { get; set; }
        public MsDiagnosis Diagnosis { get; set; }
        public string MsComments { get; set; }
        public string MedicalComments { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime AnalysisDate { get; set; }
        
        public List<AdditionalReportData> AdditionalData { get; set; }

        public Exports()
        { }

        public Exports(LimpXml msData, Form1 form1 = null)
        {
            if (form1 != null)
            {
                Patient = new MsPatient { Id = form1.patientTextBox.Text };
                
                MedicalComments = form1.medCommentsTextBox.Text;
                Diagnosis = new MsDiagnosis() { Name = "Verdict from Classificator", Id = 0 };
                Tissue = new MsTissue { Label = form1.tissueTextBox.Text, Id = 0 };
            }
            else
            {
                Patient = new MsPatient { Id = "Test patient" };

                MedicalComments = "Test medical comment";
                Diagnosis = new MsDiagnosis() { Name = "Verdict from Classificator", Id = -1 };
                Tissue = new MsTissue { Label = "test tissue", Id = -1 };
            }
            MsComments = GetComments(msData.msRun.DataProcessing[0].comment);
            ReportDate = DateTime.Now;
            AnalysisDate = msData.msRun.StartTimeDt;
            AdditionalData = new List<AdditionalReportData>();
            
            foreach (var param in Settings.Default.LimpReportData)
            {
                AdditionalData.Add(GetParam(param, msData.msRun.DataProcessing[0].processingOperation)); // new LIMPFileReader.Helpers.AdditionalReportData() { Name = GetName(param), Place = GetPlace(param)}
            }
        }

        private AdditionalReportData GetParam(string param, List<processingOperation> ap)
        {
            try
            {
                var paramParts = param.Split(new[] { ';' });
                if (!ap.Any(p => paramParts[0].Contains(p.name)))
                    return new AdditionalReportData() { Name = "empty", Place = PlaceHolder.Footer, Value = "empty value" };
                else
                    return new AdditionalReportData()
                    {
                        Name = paramParts[0].Split(new[] { ',' })[0],
                        Place = GetPlace(paramParts[1]),
                        Value = ap.First(p => paramParts[0].ToLower().Contains(p.name.ToLower())).value
                    };
            }
            catch
            {
                return new AdditionalReportData() { Name = "empty", Place = PlaceHolder.Footer, Value = "empty value" };
            }
        }

        private PlaceHolder GetPlace(string v)
        {
            switch(v.ToLower())
            {
                case "footer":
                    return PlaceHolder.Footer;
                case "header":
                    return PlaceHolder.Header;
                default:
                    return PlaceHolder.Common;
            }
        }

        private string GetComments(string[] comments)
        {
            var retStr = string.Join("; ", comments);
            if (string.IsNullOrWhiteSpace(retStr))
                return "Not specified";
            return retStr;
        }

        public static string ToPdf(Exports data, string exportFile, Form1 form1 = null, Logger logger = null, string templatePath = null)
        {
            logger = LogManager.GetCurrentClassLogger();
            string res = "OK";
            try
            {
                //var writer = new PdfWriter(exportFile);
                //using (var pdf = new PdfDocument(writer))
                //{
                //    var doc = new Document(pdf);
                //    doc.Add(new Paragraph(msData.msRun.parentFile[0].fileName));
                //}
                //var expForm = new Forms.PrintPreview(msData);
                FileInfo fi = new FileInfo(exportFile);
                //if (fi.Exists)
                //    fi.Delete();
                //var fs = fi.Create();
                
                XmlSerializer ser = new XmlSerializer(data.GetType());
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                var pdfStreamer = new PdfMergeStreamer();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("AnalysisDate", data.AnalysisDate);
                dictionary.Add("ReportDate", data.ReportDate);
                dictionary.Add("PatientId", data.Patient.Id);
                dictionary.Add("Tissue", data.Tissue.Label);
                dictionary.Add("MedicalComments", data.MedicalComments);
                dictionary.Add("TechInfo", data.AdditionalData);
                LiveCharts.Wpf.CartesianChart chart = null;
                if (form1 != null)
                    chart = form1.cartesianChart1;
                res = pdfStreamer.FillPDF(data, fi, logger, templatePath, chart);

                //fs.Flush();
                //fs.Close();

                //ser.Serialize(writer, data);
                
                //stream.Position = 0;
                //var sr = new StreamReader(stream);
                //var str = sr.ReadToEnd();
                //stream.Close();
                //writer.Close();
                //expForm.Show(form1);
            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        internal static string ToMzXml(object msData, string fileName)
        {
            string res = "OK";
            try
            {
                //msData.Serialize(fileName);
                XmlSerializer ser = new XmlSerializer(msData.GetType());

                TextWriter writer = new StreamWriter(fileName);
                ser.Serialize(writer, msData);
                writer.Close();

            }
            catch (Exception e)
            {
                res = e.Message;
            }
            return res;
        }

        //internal string ToPdf(string fileName)
        //{
        //    string res = "OK";
        //    try
        //    {
        //        //var writer = new PdfWriter(exportFile);
        //        //using (var pdf = new PdfDocument(writer))
        //        //{
        //        //    var doc = new Document(pdf);
        //        //    doc.Add(new Paragraph(msData.msRun.parentFile[0].fileName));
        //        //}
        //        //var expForm = new Forms.PrintPreview(msData);

        //        var

        //        XmlSerializer ser = new XmlSerializer(this.GetType());
        //        var stream = new MemoryStream();
        //        TextWriter writer = new StreamWriter(stream);
        //        ser.Serialize(writer, msData);

        //        stream.Position = 0;
        //        var sr = new StreamReader(stream);
        //        var str = sr.ReadToEnd();
        //        writer.Close();
        //        //expForm.Show(form1);
        //    }
        //    catch (Exception e)
        //    {
        //        res = e.Message;
        //    }
        //    return res;
        //}
    }

    public enum ReportFields
    {
        AnalysisDate = 0,
        AnalysisTime = 1,
        ReportDate = 2,
        ReportTime = 3,
        PatientId = 4,
        Tissue = 5,
        MedicalComments = 6,
        TechInfo = 7
    }

}
