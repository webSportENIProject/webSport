using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class Point 
    {
        public int Id { get; set; }

        public string Titre { get; set; }

        public int Ordre { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public int CourseId { get; set; }

        public int TypePointId { get; set; }


        public virtual Race Course { get; set; }
        public virtual TypePoint TypePoint { get; set; }


        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(Point)))
            {
                return Id == ((Point)obj).Id;
            }
            else
            {
                return false;
            }
        }

    }
}
