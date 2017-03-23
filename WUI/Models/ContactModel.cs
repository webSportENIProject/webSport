using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le {0} est requis")]
        public string Nom { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "L' {0} est requis")]
        public string Email { get; set; }

        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Le {0} est requis")]
        public string Titre { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Le {0} est requis")]
        public string Message { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

    }
}
