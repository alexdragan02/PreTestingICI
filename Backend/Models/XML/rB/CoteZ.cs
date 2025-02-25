using System.Xml.Serialization;

namespace Backend.Models.XML.rB
{
    public class CoteZ
    {
        [XmlAttribute("cota")]
        public int Cota { get; set; }

        [XmlAttribute("valOp")]
        public float ValOp { get; set; }

        [XmlAttribute("tva")]
        public float Tva { get; set; }
    }
}
