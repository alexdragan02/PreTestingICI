using System.Xml.Serialization;

namespace Backend.Models.ReportZ
{
    public class Event
    {
        [XmlAttribute("dataI")]
        public string StartDate { get; set; }

        [XmlAttribute("dataF")]
        public string EndDate { get; set; }

        [XmlAttribute("tipE")]
        public int EventType { get; set; }
    }
}
