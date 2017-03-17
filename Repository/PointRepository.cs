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
    /// Classe contenant les méthodes spécifiques aux "Point"
    /// </summary>
    public class PointRepository : GenericRepository<PointEntity>
    {
        #region Constructor

        public PointRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        //CREATE
        public int Add(Point element)
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

        //GET BY ID
        public Point GetById(int id)
        {
            var point = this.GetByIdPrivate(id);
            return point != null ? point.ToBo() : null;
        }

        private PointEntity GetByIdPrivate(int id)
        {
            return base.Where(x => x.Id == id).SingleOrDefault();
        }

        //UPDATE
        public void Update(Point element)
        {
            var pointToUpdate = this.GetByIdPrivate(element.Id);
            pointToUpdate.Titre = element.Titre;
            pointToUpdate.Ordre = element.Ordre;
            pointToUpdate.Longitude = element.Longitude;
            pointToUpdate.Latitude = element.Latitude;
            pointToUpdate.CourseId = element.Course.Id;
            pointToUpdate.TypePointId = element.TypePoint.Id;
            base.Update(pointToUpdate);
        }

        //DELETE
        public void Remove(int id)
        {
            var pointToDelete = this.GetByIdPrivate(id);
            base.Remove(pointToDelete);
        }

        //GET ALL
        public List<Point> GetAllItems()
        {
            return base.GetAll().ToBos();
        }
        #endregion
    }
}
