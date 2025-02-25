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
        private DateTime DataI { get; set; }

        [XmlAttribute("dataF")]
        private DateTime DataF { get; set; }

        [XmlAttribute("tipE")]
        private int TipE { get; set; }
    }
}
