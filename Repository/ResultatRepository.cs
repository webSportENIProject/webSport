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

        public Resultat GetById(int id)
        {
            return base.Where(x => x.id == id).SingleOrDefault().ToBo();
        }

        public List<Resultat> GetAllByCourseAndPersonne(int idCourse, int idPersonne)
        {
            return base.Where(x => x.idCourse == idCourse).Where(x => x.idPersonne == idPersonne).ToList().ToBos();
        }

        #endregion
    }
}
