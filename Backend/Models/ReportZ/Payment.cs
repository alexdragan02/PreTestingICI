using System.Xml.Serialization;

namespace Backend.Models.ReportZ
{
    public class Payment
    {
        [XmlAttribute("tipP")]
        public int PaymentType { get; set; }

        [XmlAttribute("valPl")]
        public decimal Amount { get; set; }

        [XmlAttribute("monPl")]
        public string Currency { get; set; } = "RON";
    }
}
