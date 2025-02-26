using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Backend.Models.XML.rB
{
    public class RB
    {
        [XmlAttribute("idR")]
        public string IdR { get; set; }

        [XmlAttribute("nrAv")]
        public int NrAv { get; set; }

        [XmlAttribute("nrB")]
        public int NrB { get; set; }

        [XmlAttribute("totB")]
        public float TotB { get; set; }

        [XmlAttribute("nrBC")]
        public int NrBC { get; set; }

        [XmlAttribute("totBC")]
        public float TotBC { get; set; }

        [XmlAttribute("nrA")]
        public int NrA { get; set; }

        [XmlAttribute("totA")]
        public float TotA { get; set; }

        [XmlAttribute("nrR")]
        public int NrR { get; set; }

        [XmlAttribute("totR")]
        public float TotR { get; set; }

        [XmlAttribute("nrM")]
        public int NrM { get; set; }

        [XmlAttribute("totM")]
        public float TotM { get; set; }

        [XmlAttribute("totTva")]
        public float TotTva { get; set; }

        [XmlAttribute("totTvaC")]
        public float TotTvaC { get; set; }

        [XmlAttribute("totTaxe")]
        public float TotTaxe { get; set; }

        [XmlAttribute("totNet")]
        public float TotNet { get; set; }

        [XmlAttribute("sume_serv_in")]
        public float SumeServIn { get; set; }

        [XmlAttribute("sume_serv_out")]
        public float SumeServOut { get; set; }

        [XmlAttribute("monRef")]
        public string MonRef { get; set; }

        [XmlElement("pl")]
        public PL Pl { get; set; } = new PL();

        [XmlElement("coteZ")]
        public List<CoteZ> CoteZList { get; set; } = new List<CoteZ>();

        [XmlElement("av")]
        public AV Av { get; set; } = new AV();
    }
}
