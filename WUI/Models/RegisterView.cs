using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class RegisterView
    {
        public RaceModel Course { get; set; }
        public string message { get; set; }
    }
}
