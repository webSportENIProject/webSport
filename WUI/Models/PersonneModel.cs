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
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        [RegularExpression("^0[1-68][0-9]{8}$", ErrorMessage="La saisie ne correspond pas à un numéro de téléphone")]
        public string Phone { get; set; }

        public DateTime? DateNaissance { get; set; }

        public virtual ICollection<DisplayConfigurationModel> DisplayConfigurations { get; set; }
    }
}
