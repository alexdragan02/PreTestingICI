using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Backend.Models.ReportZ
{
    [XmlRoot("msj", Namespace = "mfp:anaf:dgti:a4203:declaratie:v1")]
    public class ReportZ
    {
        [XmlAttribute("idM")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [XmlElement("bon")]
        public List<Receipt.Receipt> Receipts { get; set; } = new();

        [XmlElement("rB")]
        public ReportSummary Summary { get; set; } = new();

        [XmlElement("mE")]
        public ReportEvents Events { get; set; } = new();
    }
}
