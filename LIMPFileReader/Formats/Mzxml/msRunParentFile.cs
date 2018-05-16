
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunParentFile
    {

        private string fileNameField;

        private msRunParentFileFileType fileTypeField;

        private string fileSha1Field;

        private string valueField;

        [XmlAttribute("fileName")]
        public string fileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }
        [XmlAttribute("fileType")]
        public msRunParentFileFileType fileType
        {
            get
            {
                return this.fileTypeField;
            }
            set
            {
                this.fileTypeField = value;
            }
        }
        [XmlAttribute("fileSha1")]
        public string fileSha1
        {
            get
            {
                return this.fileSha1Field;
            }
            set
            {
                this.fileSha1Field = value;
            }
        }

        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        
    }
}
