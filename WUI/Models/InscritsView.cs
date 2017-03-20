using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class InscritsView
    {
        public List<PersonneModel> personnes { get; set; }
        public RaceModel Course { get; set; }
        public int nbInscrits { get; set; }
    }
}
