using System.Collections.Generic;
using System.Xml.Serialization;

namespace Backend.Models.ReportZ
{
    public class ReportEvents
    {
        [XmlAttribute("nrB")]
        public int NumberOfEvents { get; set; }

        [XmlElement("ev")]
        public List<Event> Events { get; set; } = new();
    }
}
