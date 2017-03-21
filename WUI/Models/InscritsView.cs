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
        public Dictionary<DateTime, int> inscriptions { get; set; }
        public RaceModel Course { get; set; }
        public Pager Pager { get; set; }
        public int nbInscrits { get; set; }
        public int TA20 { get; set; }
        public int TA21_30 { get; set; }
        public int TA31_40 { get; set; }
        public int TA41_50 { get; set; }
        public int TA51_60 { get; set; }
        public int TA61 { get; set; }
    }
}
