using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class MgtCompetitor
    {
        private List<Competitor> _competitors;


        public MgtCompetitor()
        {
            // TODO : Aller chercher ces informations en BDD via la DAL

            _competitors = new List<Competitor>
            {
                new Competitor
                {
                    Id = 1,
                    Nom = "Zidane",
                    Prenom = "Zinedine",
                    DateNaissance = new DateTime(1975, 10, 1),
                    Email = "zinedine.zidane@gmail.com",
                    Phone = "0102030405"
                },
                new Competitor
                {
                    Id = 2,
                    Nom = "Djokovik",
                    Prenom = "Novak",
                    DateNaissance = new DateTime(1980, 5, 21),
                    Email = "djokovik-novak@outlook.com",
                    Phone = "0506070809"
                },
                new Competitor
                {
                    Id = 3,
                    Nom = "Trinh-Duc",
                    Prenom = "François",
                    DateNaissance = new DateTime(1982, 1, 3),
                    Email = "ftrinhduc@ffr.fr",
                    Phone = "0304050607"
                }
            };
        }


        public List<Competitor> GetCompetitor()
        {
            return _competitors;
        }

        public Competitor GetCompetitor(int id)
        {
            return _competitors.Find(x => x.Id == id);
        }
    }
}
