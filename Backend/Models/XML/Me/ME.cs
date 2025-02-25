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
        private int NrB { get; set; }

        [XmlAttribute("ev")]
        private List<Ev> Ev { get; set; }
    }
}
