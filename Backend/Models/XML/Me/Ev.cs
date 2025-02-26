using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Backend.Models.XML.Me
{
    public class Ev
    {
        [XmlAttribute("dataI")]
        public DateTime DataI { get; set; }

        [XmlAttribute("dataF")]
        public DateTime DataF { get; set; }

        [XmlAttribute("tipE")]
        public int TipE { get; set; }
    }
}
