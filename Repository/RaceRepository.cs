using BO;
using DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Extensions;


namespace Repository
{
    /// <summary>
    /// Classe contenant les méthodes spécifiques aux "Race"
    /// </summary>
    public class RaceRepository : GenericRepository<RaceEntity>
    {
        #region Constructor

        public RaceRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public int Add(Race element)
        {
            try
            {
                var result = base.Add(element.ToDataEntity());
                return result.Id;
            }
            catch
            {
                return 0;
            }
        }

        public Race GetById(int id)
        {
            var race = this.GetByIdPrivate(id);
            return race != null ? race.ToBo() : null;
        }

        private RaceEntity GetByIdPrivate(int id)
        {
            return base.Where(x => x.Id == id).SingleOrDefault();
        }

        public void Update(Race element)
        {
            var raceToUpdate = this.GetByIdPrivate(element.Id);
            raceToUpdate.Title = element.Title;
            raceToUpdate.Description = element.Description;
            raceToUpdate.DateStart = element.DateStart;
            raceToUpdate.DateEnd = element.DateEnd;
            raceToUpdate.Town = element.Town;
            base.Update(raceToUpdate);
        }

        public void Remove(int id)
        {
            var raceToDelete = this.GetByIdPrivate(id);
            base.Remove(raceToDelete);
        }

        public List<Race> GetAllItems()
        {
            return base.GetAll().ToBos();
        }

        public Task<List<Race>> GetAllItemsAsync()
        {
            return Task.Run(() => base.GetAll().AsParallel().ToList().ToBos());
        }

        #endregion
    }
}
