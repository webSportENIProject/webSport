using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class ResultatView
    {
        public List<ResultatModel> resultats { get; set; }
        public RaceModel Course { get; set; }
    }
}
