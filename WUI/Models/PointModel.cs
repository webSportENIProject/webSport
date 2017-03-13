using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    /// <summary>
    /// Classe mère pour sauvegarder une position 
    /// </summary>
    public abstract class PointModel : PositionModel
    {
        public Guid Id { get; set; }
    }
}
