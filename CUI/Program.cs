using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Journée 1 :

            // Exercice 1 : Construire une liste désorganisée de Competitro
            List<Competitor> listCompetitor = new List<Competitor>
            {
                new Competitor() { Nom = "Zinedine", Prenom = "Zidane" },
                new Competitor() { Nom = "Djokovik", Prenom = "Novaz" },
                new Competitor() { Nom = "Djokovik", Prenom = "Novak" },
                new Competitor() { Nom = "Mauresmo", Prenom = "Amélie" },
                new Competitor() { Nom = "Fourcade", Prenom = "Martin" }
            };


            // Solution
            AfficherListe(listCompetitor);
            Console.WriteLine("\n\n");
            Console.WriteLine("Quel type de tri ? (1/2/3)");
            var value = Console.ReadKey();
            Console.WriteLine("Tri de la liste en cours...");

            listCompetitor.TrierListe(value.Key);

            // Etant donné que le 3ème cas ne fonctionne pas avec la fonction "TrierListe",
            // on la refait le travail ici, en travaillant sur la liste initiale
            if (value.Key == ConsoleKey.NumPad3)
            {
                listCompetitor = listCompetitor.OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
            }

            Console.WriteLine("\n\n");
            AfficherListe(listCompetitor);


            Console.Read();
        }

        public static void AfficherListe(List<Competitor> listCompetitor)
        {
            Console.WriteLine("Liste des sportifs :");
            listCompetitor.ForEach(x => Console.WriteLine("{0} {1}", x.Nom, x.Prenom));

            // La ligne ci-dessus fait la même chose que le code ci-après :
            //foreach (var comp in listCompetitor)
            //{
            //    Console.WriteLine("{0} {1}", comp.Nom, comp.Prenom);
            //}
        }
    }
}
