using System.Xml.Serialization;

namespace Backend.Models.XML.rB
{
    public class PL
    {
        [XmlAttribute("tipP")]
        public int TipP { get; set; }

        [XmlAttribute("valPl")]
        public double ValPl { get; set; }

        [XmlAttribute("monPl")]
        public string MonPl { get; set; }
    }
}
