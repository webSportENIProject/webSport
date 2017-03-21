using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class OldRaceView
    {
        public List<RaceModel> races { get; set; }
        public Pager Pager { get; set; }
    }
}
