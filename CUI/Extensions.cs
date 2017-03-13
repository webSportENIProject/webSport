using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUI
{
    public static class Extensions
    {
        public static void TrierListe(this List<Competitor> listCompetitor, ConsoleKey possibilite)
        {
            switch (possibilite)
            {
                case ConsoleKey.NumPad1:
                    // Solution 1 : avec IComparable (nécessite que la classe implémente IComparable)
                    listCompetitor.Sort();
                    break;

                case ConsoleKey.NumPad2:
                    // Solution 2 : avec les expressions lambda
                    listCompetitor.Sort((x, y) => string.Compare(x.Nom + "" + x.Prenom, y.Nom + "" + y.Prenom));
                    break;

                case ConsoleKey.NumPad3:
                default:
                    // Solution 3 : avec System.Linq
                    // Avec cette solution, la liste initiale (listCompetitor) ne sera pas modifiée
                    // => Passage du paramètre par valeur : on modifie listCompetitor, mais pas la variable fournie par l'appelant
                    listCompetitor = listCompetitor.OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                    break;
            }
        }
    }
}
