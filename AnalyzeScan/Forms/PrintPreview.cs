using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using AnalyzeScan;
using LIMPFileReader;

namespace AnalyzeScan.Forms
{
    public partial class PrintPreview : Form
    {
        private LimpXml content = new LimpXml();
        private Stream w;

        public PrintPreview(LimpXml msData)
        {
            InitializeComponent();
            content = msData;
            //webBrowser1.GoHome();
            webBrowser1.Navigate("about:blank");
            
        }

        private void PringPreview_Load(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            //doc.Body.InnerHtml = 
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
            webBrowser1.Document.Body.InnerHtml = string.Format(@"<html><body><h1>Title</h1><div>{0}</div></body></html>",
                content.msRun.parentFile[0].fileName);
            XmlSerializer serializer = new XmlSerializer(content.GetType());
            //string output;
            using (StringWriter contentWriter = new StringWriter())
            {
                try
                {
                    serializer.Serialize(contentWriter, content);
                    MemoryStream stream = new MemoryStream(ASCIIEncoding.Unicode.GetBytes(contentWriter.ToString()));
                    XPathDocument document = new XPathDocument(stream);
                    StringWriter writer = new StringWriter();
                    XslCompiledTransform transform = new XslCompiledTransform();
                    transform.Load(@"C:\Users\den\source\repos\AnalyzeScan\StyleVision\viewmzxml1.0.xslt");
                    transform.Transform(document, null, writer);
                    webBrowser1.Document.Body.InnerHtml = writer.ToString();


                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            
        }
    }
}
