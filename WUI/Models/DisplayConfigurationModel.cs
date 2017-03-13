using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WUI.Models
{
    /// <summary>
    /// Classe gérant la configuration de l'affichage pour un device (desktop, tablette, mobile, montre connectée...) pour un utilisateur
    /// Dans l'idée, un Id sera attribué à un device
    /// </summary>
    public class DisplayConfigurationModel
    {
        public Guid Id { get; set; }

        public int PersonneId { get; set; }

        public PersonneModel Person { get; set; }

        /// <summary>
        /// Nom du device qui utilise cette configuration
        /// </summary>
        public string DeviceName { get; set; }

        public UnitDistanceModel UnitDistance { get; set; }

        /// <summary>
        /// Affichage de la vitesse max
        /// </summary>
        public bool SpeedMax { get; set; }

        /// <summary>
        /// Affichage de la vitesse moyenne
        /// </summary>
        public bool SpeedAvg { get; set; }
    }
}
