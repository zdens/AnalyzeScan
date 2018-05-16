using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Forms.Fields;
using iText.Forms;
using AnalyzeScan.Properties;
using LIMPFileReader.Helpers;
using NLog;
using iText.Pdfa;
using iText.Kernel.Pdf.Filespec;
using System.IO;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Font;
using iText.Layout.Element;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using AnalyzeScan.DataBase;
using iText.Layout.Borders;
using LiveCharts.Wpf;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using sd = System.Drawing;
using iText.IO.Image;
using iText.IO.Util;
using System.Windows.Media;
using System.Windows;

namespace AnalyzeScan.Report
{
    public class PdfMergeStreamer
    {
        private float _leftMargin = -1;

        public string FillPDF(Exports data,
            System.IO.FileInfo outputStream, Logger logger, string templatePath = null, LiveCharts.Wpf.CartesianChart chart = null)
        {
            string resStr = "OK";
            if (templatePath == null)
                templatePath = Settings.Default.ReportTemplatePath;
            

            PdfWriter dest = new PdfWriter(outputStream);
            var templateReader = new PdfReader(templatePath);
            var stProps = new StampingProperties();
            
            PdfDocument pdfDocument = new PdfDocument(dest);
            Document doc = new Document(pdfDocument);

            _leftMargin = doc.GetLeftMargin();

            PdfDocumentInfo info = pdfDocument.GetDocumentInfo();
            info.SetTitle("Analysis report");
            info.AddCreationDate();
            try
            {
                doc.Add(GetDocHeader());

                doc.Add(GetDocDates(data));
                var line = new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine(3));
                doc.Add(new Paragraph().Add(line));
                doc.Add(GetDocBody(data));
                doc.Add(new Paragraph().Add(line));
                doc.Add(GetFooterContent(data.AdditionalData));
                if (chart != null)
                    doc.Add(GetChart(chart));
                else
                    doc.Add(GetChart(@"c:\Users\den\source\repos\AnalyzeScan\TestApp\bin\x86\Debug\chart.PNG"));
            }
            catch (Exception e)
            {
                resStr = "Exception occurred: " + e.Message;
                logger.Error(resStr);
                
            }
            pdfDocument.Close();
            return resStr;
            
        }

        private Paragraph GetChart(string v)
        {
            Paragraph p = new Paragraph();
            p.Add(new Text("Total Ion Current:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(16));
            p.Add(new Text(Environment.NewLine));
            p.Add(RenderChart(v));
            return p;
        }

        private Image RenderChart(string v)
        {
            var imgData = ImageDataFactory.Create(v);
            var img2Xobject = new iText.Kernel.Pdf.Xobject.PdfImageXObject(imgData);

            var image = new Image(img2Xobject);
            //image.ScaleToFit(400, 400);
            image.SetAutoScaleWidth(true);
            image.SetPadding(10).SetProperty(iText.Layout.Properties.Property.FLOAT, iText.Layout.Properties.FloatPropertyValue.NONE);
            return image;
        }

        private Paragraph GetChart(CartesianChart chart)
        {
            Paragraph p = new Paragraph();
            p.Add(new Text("Total Ion Current:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(16));
            p.Add(new Text(Environment.NewLine));
            p.Add(RenderChart(chart));
            return p;
        }

        private Image RenderChart(CartesianChart chart)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Default);
            //RenderTargetBitmap rtb2 = new RenderTargetBitmap((int)chart.ActualWidth, (int)chart.ActualHeight);
            //var rtb2 = CaptureScreen(chart.RenderSize);

            //chart.Measure(chart.DesiredSize);
            //container.

            rtb.Render(chart);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);
            //var strBytes = StreamUtil.InputStreamToArray(stream);
            //var imgData = iText.IO.Image.ImageDataFactory.Create(strBytes, );
            var imgData = ImageDataFactory.Create(sd.Image.FromStream(stream), null);
            var img2Xobject = new iText.Kernel.Pdf.Xobject.PdfImageXObject(imgData);
            
            var image = new Image(img2Xobject);
            //image.ScaleToFit(400, 400);
            image.SetAutoScaleWidth(true);
            image.SetPadding(10).SetProperty(iText.Layout.Properties.Property.FLOAT, iText.Layout.Properties.FloatPropertyValue.NONE);
            return image;
            //myImage.Source = bmp;
        }
        private static BitmapSource CaptureScreen(CartesianChart target, double dpiX, double dpiY)
        {
            if (target == null)
            {
                return null;
            }
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)(bounds.Width * dpiX / 96.0),
                                                            (int)(bounds.Height * dpiY / 96.0),
                                                            dpiX,
                                                            dpiY,
                                                            PixelFormats.Pbgra32);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(target);
                ctx.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);
            return rtb;
        }
        private Paragraph GetFooterContent(List<AdditionalReportData> additionalData)
        {
            Paragraph p = new Paragraph();
            p.Add(new Text("Additional information:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(16));
            p.Add(GetMetaData(additionalData));
            
            return p;
        }

        private Paragraph GetMetaData(List<AdditionalReportData> additionalData)
        {
            Paragraph p = new Paragraph();
            p.SetMarginLeft(_leftMargin + 10);
            float[] widths = new float[] { 200f, 400f };
            var table = GetTable(2, 600f);
            foreach (var rd in additionalData)
            {
                if (rd.Name.ToLower().Equals("empty")) continue;
                table.AddCell(GetCell(rd.Name, widths[0]));
                table.AddCell(GetCell(rd.Value, widths[1]));
            }
            p.Add(table);
            return p;
        }

        private Paragraph GetDocBody(Exports data)
        {
            Paragraph p = new Paragraph();
            p.Add(new Text("Patient information:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(16));
            p.Add(GetPatientInfo(data.Patient));
            p.Add(new Text("Tissue information:")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                .SetFontSize(16));
            p.Add(GetTissueInfo(data.Tissue, data.Diagnosis));

            return p;
        }

        private Paragraph GetTissueInfo(MsTissue tissue, MsDiagnosis diag)
        {
            Paragraph p = new Paragraph();
            p.SetMarginLeft(_leftMargin + 10);
            float[] widths = new float[] { 200f, 400f };
            var table = GetTable(2, 600f);
            table.AddCell(GetCell("Tissue name", widths[0]));
            table.AddCell(GetCell(tissue.Label, widths[1]));

            table.AddCell(GetCell("Diagnosis", widths[0]));
            table.AddCell(GetCell(diag.Name, widths[1]));
            //table.Complete();
            p.Add(table);
            return p;
        }

        private Paragraph GetPatientInfo(MsPatient patient)
        {
            Paragraph p = new Paragraph();
            p.SetMarginLeft(_leftMargin + 10);
            float[] widths = new float[] { 200f, 400f };
            var table = GetTable(2, 600f);
            table.AddCell(GetCell("Patient", widths[0]));
            table.AddCell(GetCell(patient.Id, widths[1]));
            //table.Complete();
            p.Add(table);
            return p;
        }

        private Paragraph GetDocDates(Exports data)
        {
            Paragraph p = new Paragraph();
            var table = GetTable(2, 500f, ColorConstants.BLUE);
            float[] widths = new float[] { 250f, 250f };
            table.AddCell(GetCell("Report date: " + data.ReportDate.ToString("dd-MM-yyyy HH:mm:ss"), widths[0]).SetBorder(Border.NO_BORDER));
            table.AddCell(GetCell("Analysis date: " + data.AnalysisDate.ToString("dd-MM-yyyy HH:mm:ss"), widths[1]).SetBorder(Border.NO_BORDER));
            //table.(null);
            p.Add(table);
            p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            return p.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
        }

        //private object GetTable(int v1, float v2, Color bLUE)
        //{
        //    throw new NotImplementedException();
        //}

        private Cell GetCell(string innerText, float width)
        {
            return new Cell()
                .SetWidth(width)
                .Add(new Paragraph(new Text(innerText)));
        }
        private Table GetTable(int numOfColumns, float width, iText.Kernel.Colors.Color color = null)
        {
            var table = new Table(numOfColumns);
            table.SetPaddings(5, 5, 5, 5);
            
            if (color != null)
                table.SetFontColor(color);
            return table;
        }
        private Paragraph GetDocHeader()
        {
            Paragraph p = new Paragraph();
            Text t = new Text("Analysis report").SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
            t.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            t.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            p.Add(t.SetFontSize(20));
            return p.SetMargins(10, 10, 10, 10).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
        }

        private PdfFont GetTimesFont()
        {
            var bfTimes = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            //PdfFontFactory bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            //PdfFont ret = new PdfFont(bfTimes, 12, ITALIC, Color.RED);
            return bfTimes;
        }
        public static PdfFont GetTahoma()
        {
            //return PdfFontFactory.CreateFont(StandardFonts);
            var fontName = "Tahoma";
            if (!PdfFontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\tahoma.ttf";
                PdfFontFactory.Register(fontPath);
            }
            return PdfFontFactory.CreateRegisteredFont(fontName, PdfEncodings.IDENTITY_H, true);
        }
    }
}
