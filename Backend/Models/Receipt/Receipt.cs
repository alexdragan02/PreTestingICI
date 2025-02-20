using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Backend.Models.Receipt
{
    [XmlRoot("bon")]
    public class Receipt
    {
        [XmlAttribute("idB")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [XmlAttribute("totB")]
        public decimal Total { get; set; }

        [XmlAttribute("totTva")]
        public decimal TotalVAT { get; set; }

        [XmlElement("cote")]
        public List<TaxRate> TaxRates { get; set; } = new();
    }
}
