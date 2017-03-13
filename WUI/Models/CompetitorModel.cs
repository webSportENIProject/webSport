using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class CompetitorModel : PersonneModel
    {
        public RaceModel Race { get; set; }
    }
}
