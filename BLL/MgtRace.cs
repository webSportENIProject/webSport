using BO;
using DAL;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BLL
{
    public class MgtRace
    {
        
        private static MgtRace _instance;

        public static MgtRace GetInstance()
        {
            if (_instance == null)
                _instance = new MgtRace();
            return _instance;
        }    

        private UnitOfWork _uow { get; set; }     

        private MgtRace()
        {  
            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = UnitOfWork.GetInstance();           
        }

        public Race AddRace(Race race)
        {
            if (race != null)
            {
              
                int lastId = this._uow.RaceRepo.Add(race);
                if (lastId > 0)
                {
                    race.Id = lastId;
                    return race;
                }
                else {
                    return null;
                }               
            }

            return null;
        }

        public List<Race> GetAllItems()
        {
            List <Race> races = this._uow.RaceRepo.GetAllItems();
            return races;
        }

        public List<Race> GetAllItemsWithParticipants()
        {
            List<Race> races = GetAllItems();
            foreach (Race item in races)
            {
                item.Participants = this._uow.ParticipantRepo.GetAllItemsByIdRace(item.Id);
            }
            return races;
        }


        public Race GetRace(int id)
        {           
            return this._uow.RaceRepo.GetById(id);
        }

        public Race GetRaceWithPoints(int id) {
            Race race = GetRace(id);
            race.Points = this._uow.PointRepo.GetAllItemsByIdRace(id);
            foreach (Point item in race.Points) {
                item.TypePoint = this._uow.TypePointRepo.GetById(item.TypePointId);
            }
            return race;
        }


        public bool UpdateRace(Race race)
        {
            if (race == null || race.Id < 1) return false;
            
            this._uow.RaceRepo.Update(race);
            this._uow.Save();
            return true;
        }

        public bool RemoveRace(int id)
        {
            if (id < 1) return false;

            //Suppression des resultats
            this._uow.ResultatRepo.RemoveAllResultatByCourse(id);
            //Suppression des points
            this._uow.PointRepo.RemoveAllPointByCourse(id);
            //Suppression des participation
            this._uow.ParticipantRepo.RemoveAllParticipationByCourse(id);
            //Suppresion de la course
            this._uow.RaceRepo.Remove(id);
            this._uow.Save();

            return true;
        }

        public void addInscription(int idRace, int idUser) {
            Personne personne = this._uow.PersonneRepo.GetByIdUserTable(idUser);
            this._uow.ParticipantRepo.AddCompetiteur(idRace, personne.Id);
        }

    }
}