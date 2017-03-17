using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class ResultatModel 
    {
        public int id { get; set; }
        public int idPoint { get; set; }
        public int idCourse { get; set; }
        public int idPersonne { get; set; }
        public System.DateTime temps { get; set; }

        [Display(Name = "Temps au point")]
        public String tempsPoint { get; set; }

        public PointModel Point { get; set; }
    }
}
