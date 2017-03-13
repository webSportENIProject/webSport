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
        #region Singleton

        private static MgtRace _instance;
        public static MgtRace GetInstance()
        {
            if (_instance == null)
                _instance = new MgtRace();
            return _instance;
        }

        #endregion

        #region Propriétés

        private List<Race> _listRace;

        private Comparison<Race> _delegueTrier;

        private UnitOfWork _uow { get; set; }

        #endregion

        #region Constructeur

        public MgtRace()
        {
            // Récupération des données via la DAL (informations stockées dans un fichier XML)
            //_listRace = XmlRace.GetInstance().GetRace();

            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = new UnitOfWork();
            _listRace = this._uow.RaceRepo.GetAllItems();
        }

        public MgtRace(Comparison<Race> delegueTrier)
            : this()
        {
            _delegueTrier = delegueTrier;
        }

        #endregion

        #region Create

        public bool AddRace(Race race)
        {
            if (race != null
                && _listRace.All(x => x.Id != race.Id))
            {
                // Récupération des données via la DAL (informations stockées dans un fichier XML)
                //if (XmlRace.GetInstance().AddRaceXml(race))


                // Récupération des données via la DAL (informations stockées dans une base de données SQL)

                // 1/ Si la BLL référence directement la DAL, alors on peut utiliser le code suivant
                //int lastId = DbRace.GetInstance().AddRace(race);
                // 2/ Si la BLL référence la Repository qui référence elle-même la DAL
                int lastId = this._uow.RaceRepo.Add(race);
                if (lastId > 0)
                {
                    race.Id = lastId;
                    _listRace.Add(race);
                }
                return true;
            }

            return false;
        }

        #endregion

        #region Read

        public List<Race> GetRace()
        {
            return _listRace;
        }

        public async Task<List<Race>> GetAllRaceAsync()
        {
            // Pour charger la liste dans notre View, il serait préférable de
            // faire un appel AJAX avec jQuery à une méthode de RaceController,
            // laquelle appellerait cette méthode GetAllRaceAsync()
            return await this._uow.RaceRepo.GetAllItemsAsync();
        }

        public List<Race> GetRace(string term)
        {
            return _listRace.Where(x => x.Title.Contains(term)).ToList();
        }

        public Race GetRace(int id)
        {
            // On pourrait récupérer l'élément à partir de la liste chargée en mémoire
            //return _listRace.Find(x => x.Id == id);

            // On va charger l'élément à partir d'une procédure stockée en BDD
            // Cela nous permettra d'avoir les données à jour

            // 1/ Si la BLL référence directement la DAL, alors on peut utiliser le code suivant
            //return DbRace.GetInstance().GetRace(id);
            // 2/ Si la BLL référence la Repository qui référence elle-même la DAL
            return this._uow.RaceRepo.GetById(id);
        }

        #endregion

        #region Update

        public bool UpdateRace(Race race)
        {
            if (race == null || race.Id < 1) return false;

            // 1/ Si la BLL référence directement la DAL, alors on peut utiliser le code suivant
            //var result = DbRace.GetInstance().UpdateRace(race);
            // 2/ Si la BLL référence la Repository qui référence elle-même la DAL
            this._uow.RaceRepo.Update(race);
            if (this._uow.Save())
            {
                var raceToUpdate = _listRace.SingleOrDefault(x => x.Id == race.Id);
                if (raceToUpdate != null)
                {
                    raceToUpdate.Title = race.Title;
                    raceToUpdate.Description = race.Description;
                    raceToUpdate.DateStart = race.DateStart;
                    raceToUpdate.DateEnd = race.DateEnd;
                    raceToUpdate.Town = race.Town;
                }
            }

            return true;
        }

        #endregion

        #region Delete

        public bool RemoveRace(int id)
        {
            if (id < 1) return false;

            // 1/ Si la BLL référence directement la DAL, alors on peut utiliser le code suivant
            //var result = DbRace.GetInstance().RemoveRace(id);
            // 2/ Si la BLL référence la Repository qui référence elle-même la DAL
            this._uow.RaceRepo.Remove(id);
            if (this._uow.Save())
            {
                var raceToDelete = _listRace.SingleOrDefault(x => x.Id == id);
                if (raceToDelete != null)
                {
                    _listRace.Remove(raceToDelete);
                }
            }

            return true;
        }

        #endregion

        #region Méthodes de tri

        public List<Race> SortByDateStart()
        {
            // Tri de ma liste avec IComparable
            _listRace.Sort();

            return _listRace;
        }

        public List<Race> SortByTitle()
        {
            // Tri de ma liste avec un délégué
            _listRace.Sort(_delegueTrier);

            return _listRace;
        }

        public List<Race> SortByTown(Func<Race, Race, int> func)
        {
            // Tri de ma liste avec une expression lambda
            // REMARQUE  : on utilise le delegate proposé par le framework
            // .NET "Comparison<T>" lequel sait interprêter une "Func"
            _listRace.Sort(new Comparison<Race>(func));

            return _listRace;
        }

        #endregion
    }
}