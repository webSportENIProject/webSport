using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WUI.Models.Attributes;

namespace WUI.Models
{
    public class MailModel
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Email { get; set; }

        public string Titre { get; set; }
        
        public string Message { get; set; }

    }
}
