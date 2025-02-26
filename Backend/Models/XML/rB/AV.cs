using System.Xml.Serialization;

namespace Backend.Models.XML.rB
{
    public class AV
    {
        [XmlAttribute("data")]
        public DateTime Data { get; set; } 

    }
}
