using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class PersonneModel 
    {
        [Display(Name = "Identifiant")]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        [RegularExpression("^0[1-68][0-9]{8}$", ErrorMessage="La saisie ne correspond pas à un numéro de téléphone")]
        public string Phone { get; set; }

        public int UserTable { get; set; }

        [Display(Name = "Distance")]
        public string distance { get; set; }

        [Display(Name = "Date de Naissance")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateNaissance { get; set; }

        public ParticipantModel participant { get; set; }
    }

}
