using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TypePoint
    {
        public int Id { get; set; }

        public string Libelle { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(TypePoint)))
            {
                return Id == ((TypePoint)obj).Id;
            }
            else
            {
                return false;
            }
        }
    }
}
