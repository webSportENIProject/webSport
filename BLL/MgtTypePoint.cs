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
    public class MgtTypePoint
    {
        private static MgtTypePoint _instance;

        public static MgtTypePoint GetInstance()
        {
            if (_instance == null)
                _instance = new MgtTypePoint();
            return _instance;
        }

        private UnitOfWork _uow { get; set; }

        private MgtTypePoint()
        {
            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = new UnitOfWork();
        }

        public bool AddTypePoint(TypePoint typePoint)
        {
            if (typePoint != null)
            {

                int lastId = this._uow.TypePointRepo.Add(typePoint);
                if (lastId > 0)
                {
                    typePoint.Id = lastId;
                }
                return true;
            }

            return false;
        }

        public TypePoint GetPoint(int id)
        {
            return this._uow.TypePointRepo.GetById(id);
        }

        public bool UpdateTypePoint(TypePoint typePoint)
        {
            if (typePoint == null || typePoint.Id < 1) return false;

            this._uow.TypePointRepo.Update(typePoint);
            this._uow.Save();
            return true;
        }      

    }
}
