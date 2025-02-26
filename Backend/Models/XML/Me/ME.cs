using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Models.XML.Me
{
    public class ME
    {
        [XmlAttribute("nrB")]
        public int NrB { get; set; }

        [XmlElement("ev")]
        public List<Ev> Ev { get; set; } = new List<Ev>();
    }
}
