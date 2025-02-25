using System.Xml.Serialization;

namespace Backend.Models.XML.Bon
{
    public class Cote
    {
        [XmlAttribute("cota")]
        public int Cota { get; set; }

        [XmlAttribute("tva")]
        public decimal Tva { get; set; }
    }
}
