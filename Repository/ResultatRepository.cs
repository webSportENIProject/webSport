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
    public class ResultatRepository : GenericRepository<ResultatEntity>
    {
        #region Constructor

        public ResultatRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public int Add(Resultat element)
        {
            try
            {
                var result = base.Add(element.ToDataEntity());
                return result.id;
            }
            catch
            {
                return 0;
            }
        }

        public void AddAll(List<Resultat> elements)
        {
            foreach (Resultat bo in elements)
            {
                Add(bo);
            }
        }

        //GET
        public Resultat GetById(int id)
        {
            var resultat = this.GetByIdPrivate(id);
            return resultat != null ? resultat.ToBo() : null;
        }
        private ResultatEntity GetByIdPrivate(int id)
        {
            return base.Where(x => x.id == id).SingleOrDefault();
        }

        //GET ALL BY ID COURSE AND ID PERSONNE
        public List<Resultat> GetAllByCourseAndPersonne(int idCourse, int idPersonne)
        {
            return base.Where(x => x.idCourse == idCourse).Where(x => x.idPersonne == idPersonne).ToList().ToBos();
        }

        //Récupération de la liste des résultats pour un point d'une course
        public List<Resultat> GetAllByPoint(int idPoint)
        {
            return base.Where(x => x.idPoint == idPoint).ToList().ToBos();
        }

        //DELETE
        public void Remove(int id)
        {
            var resultatToDelete = this.GetByIdPrivate(id);
            base.Remove(resultatToDelete);
        }

        public void RemoveAllResultatByCourse(int idCourse)
        {
            List<ResultatEntity> entities = base.Where(x => x.idCourse == idCourse).ToList();
            foreach (ResultatEntity entity in entities)
            {
                base.Remove(entity);
            }
        }

        #endregion
    }
}
