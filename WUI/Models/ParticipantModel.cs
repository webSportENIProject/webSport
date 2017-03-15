using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class ParticipantModel
    {
        public int PersonneId { get; set; }

        public int CourseId { get; set; }

        public bool EstCompetiteur { get; set; }

        public bool EstOrganisateur { get; set; }

    }
}
