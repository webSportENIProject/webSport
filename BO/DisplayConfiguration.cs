using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BO
{
    // Il est possible de définir une classe comme étant l'élément racine de notre fichier XML
    // On pourra notamment lui préciser son namespace
    //[XmlRoot("displayconfiguration", Namespace = "http://www.eni-ecole.fr/displayconf", IsNullable = true)]*

    /// <summary>
    /// Classe gérant la configuration de l'affichage pour un device (desktop, tablette, mobile, montre connectée...) pour un utilisateur
    /// Dans l'idée, un Id sera attribué à un device
    /// </summary>
    [Serializable]
    [DataContract]
    public class DisplayConfiguration
    {
        [XmlElement("id")]
        [DataMember(Name = "id", IsRequired = true)]
        public Guid Id { get; set; }

        [XmlElement("personneid")]
        [DataMember(Name = "personneid", IsRequired = true)]
        public int PersonneId { get; set; }

        [XmlIgnore]
        public Personne Person { get; set; }

        /// <summary>
        /// Nom du device qui utilise cette configuration
        /// </summary>
        [XmlElement("devicename")]
        [DataMember(Name = "devicename")]
        public string DeviceName { get; set; }

        [XmlElement("unitdistance")]
        [DataMember]
        public UnitDistance UnitDistance { get; set; }

        /// <summary>
        /// Affichage de la vitesse max
        /// </summary>
        [XmlElement("speedmax")]
        [DataMember]
        public bool SpeedMax { get; set; }

        /// <summary>
        /// Affichage de la vitesse moyenne
        /// </summary>
        [XmlElement("speedavg")]
        [DataMember]
        public bool SpeedAvg { get; set; }


        public DisplayConfiguration()
        {
        }

        public DisplayConfiguration(Personne p)
        {
            Person = p;
        }
    }
}
