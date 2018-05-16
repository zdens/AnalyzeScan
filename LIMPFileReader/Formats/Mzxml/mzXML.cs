using NLog;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [XmlRoot("mzXML", Namespace = "http://sashimi.sourceforge.net/schema_revision/mzXML_2.1", IsNullable = false)]
    public class mzXML //: IXmlSerializable
    {
        //private string datetimeFormat = "dd-MM-yyyy";
        private msRun msRunField;

        private mzXMLIndex indexField;

        private long? indexOffsetField;

        private string sha1Field;

        private string xmlnsAttribute;

        public string xmlns
        {
            get { return xmlnsAttribute; }
            set { xmlnsAttribute = value; }
        }
        

        public mzXML()
        {
            this.indexField = new mzXMLIndex();
            this.msRunField = new msRun();
        }

        
        public msRun msRun
        {
            get
            {
                return this.msRunField;
            }
            set
            {
                this.msRunField = value;
            }
        }

        public mzXMLIndex index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }
        public long? indexOffset
        {
            get
            {
                return this.indexOffsetField;
            }
            set
            {
                this.indexOffsetField = value;
            }
        }

        public string sha1
        {
            get
            {
                return this.sha1Field;
            }
            set
            {
                this.sha1Field = value;
            }
        }
        
    }
}
