using System.Xml.Serialization;

namespace Backend.Models.Receipt
{
    public class TaxRate
    {
        [XmlAttribute("cota")]
        public int Rate { get; set; }

        [XmlAttribute("tva")]
        public decimal VATAmount { get; set; }
    }
}
