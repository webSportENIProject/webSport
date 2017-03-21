using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WUI.Models
{
    public class PersonneView
    { 
        public List<PersonneModel> personnes { get; set; }
        public Pager Pager { get; set; }
    }
}