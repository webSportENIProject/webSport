using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Email { get; set; }

        public string Titre { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

    }
}
