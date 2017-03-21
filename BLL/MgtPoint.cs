using BO;
using DAL;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MgtPoint
    {
        private static MgtPoint _instance;

        public static MgtPoint GetInstance()
        {
            if (_instance == null)
                _instance = new MgtPoint();
            return _instance;
        }

        private UnitOfWork _uow { get; set; }

        private MgtPoint()
        {
            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = UnitOfWork.GetInstance();
        }

        public bool AddPoint(Point point)
        {
            if (point != null)
            {

                int lastId = this._uow.PointRepo.Add(point);
                if (lastId > 0)
                {
                    point.Id = lastId;
                }
                return true;
            }

            return false;
        }

        //GET ALL
        public List<Point> GetAllItems()
        {
            List<Point> points = this._uow.PointRepo.GetAllItems();
            return points;
        }

        //Récupération des points avec la course et le type de point associés
        public List<Point> GetALLWithCourseAndTypePoint()
        {
            List<Point> points = this.GetAllItems();

            foreach (Point item in points)
            {
                item.Course = this._uow.RaceRepo.GetById(item.CourseId);
                item.TypePoint = this._uow.TypePointRepo.GetById(item.TypePointId);
            }

            return points;
        }

        public List<Point> GetAllWithCourseAndTypePointByCourse(int idcourse)
        {
            List<Point> points = this._uow.PointRepo.GetAllItemsByIdRace(idcourse);

            foreach (Point item in points)
            {
                item.Course = this._uow.RaceRepo.GetById(item.CourseId);
                item.TypePoint = this._uow.TypePointRepo.GetById(item.TypePointId);
            }

            return points;
        }

        //GET BY ID
        public Point GetPointById(int id)
        {
            return this._uow.PointRepo.GetById(id);
        }

        public Point GetPointByCourseAndOrder(int idCourse, int ordre)
        {
            return this._uow.PointRepo.GetPointByCourseAndOrder(idCourse, ordre);
        }

        //UPDATE
        public bool UpdatePoint(Point point)
        {
            if (point == null || point.Id < 1) return false;

            this._uow.PointRepo.Update(point);
            this._uow.Save();
            return true;
        }
        
        //DELETE
        public bool DeletePoint(int id)
        {
            if (id < 1) return false;

            this._uow.PointRepo.Remove(id);
            this._uow.Save();

            return true;
        }      

    }
}
