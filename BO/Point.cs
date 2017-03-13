using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Classe mère pour sauvegarder une position 
    /// </summary>
    public abstract class Point : Position
    {
        public Guid Id { get; set; }
    }
}
