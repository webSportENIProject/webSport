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
            _uow = UnitOfWork.GetInstance();
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

        public Personne GetPersonneById(int id)
        {
            return this._uow.PersonneRepo.getPersonneByUserId(id);
        }

        //GET ALL
        public List<Personne> GetAll()
        {
            List<Personne> listPersonnes = this._uow.PersonneRepo.GetAllItems();
            return (listPersonnes);
        }

        public List<Personne> GetAllItemsByLimit(int skip, int take)
        {
            List<Personne> listPersonnes = this._uow.PersonneRepo.GetAllItemsByLimit(skip, take);
            return (listPersonnes);
        }

        // UPDATE
        public void UpdatePersonne(Personne personne) {
            this._uow.PersonneRepo.Update(personne);
        }
        // DELETE
        public void RemovePersonne(int idUser)
        {
            Personne personne = this._uow.PersonneRepo.GetByIdUserTable(idUser);
            //Supprimer inscription, resultat, usertable, table password
            this._uow.ResultatRepo.RemoveAllResultatByIdPersonne(personne.Id);
            this._uow.ParticipantRepo.RemoveAllParticipationByIdPersonne(personne.Id);
            this._uow.PersonneRepo.RemoveByIdUserTable(idUser);
            this._uow.MembershipRepo.Remove(idUser);
            this._uow.UserTableRepo.Remove(idUser);
            this._uow.Save();
        }
    }
}
