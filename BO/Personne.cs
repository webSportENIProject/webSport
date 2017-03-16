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

        public int UserTable { get; set; }

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

        public Personne()
        {

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
                       && Email == ((Personne)obj).Email
                       && Nom == ((Personne)obj).Nom
                       && Prenom == ((Personne)obj).Prenom
                       && Phone == ((Personne)obj).Phone
                       && DateNaissance == ((Personne)obj).DateNaissance;
            }
            else
            {
                return false;
            }
        }
    }
}
