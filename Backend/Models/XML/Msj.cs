using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Backend.Models.XML
{
    [XmlRoot("msj",Namespace = "mfp:anaf:dgti:a4203:declaratie:v1")]
    public class Msj
    {
        [Key]
        [XmlAttribute("idM")]
        public string IdM { get; set; }

        [XmlElement("bon")]
        public Bon.Bon Bon { get; set; }=new Bon.Bon();

        [XmlElement("mE")]
        public Me.ME ME { get; set; } = new Me.ME();

        [XmlElement("rB")]
        public rB.RB RB { get; set; } = new rB.RB();

        [ForeignKey("CasaDeMarcat")]
        public int CasaDeMarcatId { get; set; }


        public virtual CasaDeMarcat CasaDeMarcat { get; set; }
    }
}
