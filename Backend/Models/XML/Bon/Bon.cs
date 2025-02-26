using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Backend.Models.XML.Bon
{
    public class Bon
    {
        [XmlAttribute("idB")]
        public string IdB { get; set; }

        [XmlAttribute("totB")]
        public decimal TotB { get; set; }

        [XmlAttribute("totTva")]
        public decimal TotTva { get; set; }

        [XmlElement("cote")]
        public List<Cote> Cote { get; set; } = new List<Cote>();
    }
}
