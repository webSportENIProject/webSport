using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class Personne 
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [NonSerialized]
        private DateTime? _dateNaissance;
        public DateTime DateNaissance
        {
            get
            {
                return _dateNaissance != null ? _dateNaissance.Value : DateTime.MinValue;
            }
            set
            {
                _dateNaissance = (DateTime?)value;
            }
        }

        [NonSerialized]
        private ICollection<DisplayConfiguration> _displayConfigurations;
        public virtual ICollection<DisplayConfiguration> DisplayConfigurations
        {
            get
            {
                return _displayConfigurations;
            }
            set
            {
                _displayConfigurations = value;
            }
        }


        public Personne()
        {
            DisplayConfigurations = new HashSet<DisplayConfiguration>();
        }

        public Personne(string email)
        {
            Email = email;
        }


        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(Personne)))
            {
                return Id == ((Personne)obj).Id
                       && Email == ((Personne)obj).Email;
            }
            else
            {
                return false;
            }
        }
    }
}
