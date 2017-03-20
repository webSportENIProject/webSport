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
    public class Race 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public string Town { get; set; }

        public int MaxParticipants { get; set; }

        public virtual List<Participant> Participants { get; set; }

        public virtual List<Point> Points { get; set; }
    }
}
