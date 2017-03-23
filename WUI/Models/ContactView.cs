using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Models
{
    public class ContactView
    {
        public List<ContactModel> contact { get; set; }
        public Pager Pager { get; set; }

    }
}
