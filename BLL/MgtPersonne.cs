using BO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MgtPersonne
    {
        private static MgtPersonne _instance;
        public static MgtPersonne GetInstance()
        {
            if (_instance == null)
                _instance = new MgtPersonne();
            return _instance;
        }


        private UnitOfWork _uow { get; set; }


        private MgtPersonne()
        {
            // Récupération des données via la DAL (informations stockées dans un fichier XML)
            //_listRace = XmlRace.GetInstance().GetRace();

            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = new UnitOfWork();
        }

        // CREATE
        public void AddPersonne(Personne personne)
        {
            this._uow.PersonneRepo.Add(personne);
        }

        //GET
        public Personne GetPersonneByIdUserTable(int id)
        {
            return this._uow.PersonneRepo.GetByIdUserTable(id);
        }

        // UPDATE

        // DELETE
    }
}
