using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class Competitor : Personne, IComparable<Competitor>, IDisposable
    {
        public Race Race { get; set; }



        public int CompareTo(Competitor other)
        {
            if (other == null)
            {
                return 1;
            }

            var initialCompetitor = this.Nom + " " + this.Prenom;
            var paramCompetitor = other.Nom + " " + other.Prenom;

            return initialCompetitor.CompareTo(paramCompetitor);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
