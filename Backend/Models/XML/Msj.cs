using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Backend.Models.XML
{
    [XmlRoot("msj",Namespace = "mfp:anaf:dgti:a4203:declaratie:v1")]
    public class Msj
    {
        [XmlAttribute("idM")]
        public string IdM;

        [XmlAttribute("bon")]
        public Bon.Bon Bon;

        [XmlAttribute("mE")]
        public Me.ME ME;

        [XmlAttribute("rB")]
        public rB.RB RB;
    }
}
