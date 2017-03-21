using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class Participant 
    {
        public int IdPersonne { get; set; }

        public int IdCourse { get; set; }

        public DateTime dateInscription { get; set; }

        public bool EstCompetiteur { get; set; }

        public bool EstOrganisateur { get; set; }
    }
}
