using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    /// <summary>
    /// Classe mère pour sauvegarder une position 
    /// </summary>
    public class PointModel
    {
        [Display(Name = "Identifiant")]
        public int Id { get; set; }

        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Le {0} est requis")]
        public string Titre { get; set; }

        [Display(Name = "Ordre")]
        [Required(ErrorMessage = "L' {0} est requis")]
        public int Ordre { get; set; }

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "La {0} est requise")]
        public double Longitude { get; set; }

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "La {0} est requise")]
        public double Latitude { get; set; }

        [Display(Name = "Course")]
        public String LibelleCourse { get; set; }

        [Display(Name = "Type de point")]
        public String LibelleTypePoint { get; set; }


        public IEnumerable<TypePoint> ListTypePointOptions;

        [DisplayName("Sélectionnez")]
        public int IdTypePoint { get; set; }

        public IEnumerable<Race> ListCourseOptions;

        [DisplayName("Sélectionnez")]
        public int IdCourse { get; set; }
    }
}
