using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public class PointView
    {
        public List<PointModel> points { get; set; }
        public int idCourse { get; set; }
    }
}
