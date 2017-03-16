using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Models
{
    public class TypePointModel 
    {
        [Display(Name = "Identifiant")]
        public int Id { get; set; }

        [Display(Name = "Libelle")]
        [Required(ErrorMessage = "Le {0} est requis")]
        public string Libelle { get; set; }

    }
}
