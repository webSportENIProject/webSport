using BO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MgtParticipant
    {
        private static MgtParticipant _instance;
        public static MgtParticipant GetInstance()
        {
            if (_instance == null)
                _instance = new MgtParticipant();
            return _instance;
        }


        private UnitOfWork _uow { get; set; }


        private MgtParticipant()
        {
            // Récupération des données via la DAL (informations stockées dans un fichier XML)
            //_listRace = XmlRace.GetInstance().GetRace();

            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = new UnitOfWork();
        }

        // CREATE
        public void AddParticipant(Participant participant)
        {
            this._uow.ParticipantRepo.AddCompetiteur(participant.IdCourse, participant.IdPersonne);
        }

        //GET
        public List<Participant> GetAll()
        {
            return this._uow.ParticipantRepo.GetAllItems();
        }

        //GET BY ID
        public List<Participant> GetAllById(int id)
        {
            return this._uow.ParticipantRepo.GetAllItemsByIdPersonne(id);
        }

        // UPDATE

        // DELETE
     }
}
