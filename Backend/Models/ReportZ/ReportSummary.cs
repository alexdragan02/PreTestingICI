using System.Xml.Serialization;

namespace Backend.Models.ReportZ
{
    public class ReportSummary
    {
        [XmlAttribute("idR")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [XmlAttribute("nrB")]
        public int NumberOfReceipts { get; set; }

        [XmlAttribute("totB")]
        public decimal TotalReceipts { get; set; }

        [XmlAttribute("totTva")]
        public decimal TotalVAT { get; set; }

        [XmlElement("pl")]
        public Payment Payment { get; set; } = new();
    }
}
