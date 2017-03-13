using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WUI.Models
{
    public class Personnage
    {
        [Display(Name = "FirstName", ResourceType = typeof(SiteRessource))]
        [Required(ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "FirstNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "FirstNameLong")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(SiteRessource))]
        [Required(ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "LastNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "LastNameLong")]
        public string LastName { get; set; }

        [Display(Name = "Age", ResourceType = typeof(SiteRessource))]
        [Required(ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "AgeRequired")]
        [Range(0, 130, ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "AgeRange")]
        public int Age { get; set; }

        [Display(Name = "Email", ResourceType = typeof(SiteRessource))]
        [Required(ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "EmailRequired")]
        [RegularExpression(".+@.+\\..+", ErrorMessageResourceType = typeof(SiteRessource), ErrorMessageResourceName = "EmailInvalid")]
        public string Email { get; set; }

        [Display(Name = "Biography", ResourceType = typeof(SiteRessource))]
        public string Biography { get; set; }
    }
}