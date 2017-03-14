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
            _uow = new UnitOfWork();           
        }

        public bool AddRace(Race race)
        {
            if (race != null)
            {
              
                int lastId = this._uow.RaceRepo.Add(race);
                if (lastId > 0)
                {
                    race.Id = lastId;
                }
                return true;
            }

            return false;
        }

        public List<Race> GetAllItems()
        {
            return this._uow.RaceRepo.GetAllItems();
        }
             

        public Race GetRace(int id)
        {           
            return this._uow.RaceRepo.GetById(id);
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

            this._uow.RaceRepo.Remove(id);
            this._uow.Save();

            return true;
        }

    }
}