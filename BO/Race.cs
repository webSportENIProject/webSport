using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BO
{
    /// <summary>
    /// Course
    /// </summary>
    [Serializable]
    public class Race : IComparable<Race>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public string Town { get; set; }

        public int MaxParticipants { get; set; }

        public virtual List<Participant> Participants { get; set; }


        public virtual List<Poi> Pois { get; set; }


        public int CompareTo(Race otherRace)
        {
            if (otherRace == null)
            {
                return 1;
            }

            return this.DateStart.CompareTo(otherRace.DateStart);
        }
    }
}
