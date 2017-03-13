using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    public enum UnitDistanceModel
    {
        Kilometers = 0,
        Miles = 1
    }

    public enum SortType
    {
        DEFAULT, // Tri par date de début
        BY_TITLE,
        BY_TOWN
    }
}
