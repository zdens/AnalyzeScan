using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("processingOperation")]
    public class processingOperation : namevalueType
    {
    }
}
