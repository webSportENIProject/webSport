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
    /// Classe contenant les méthodes spécifiques aux "TypePoint"
    /// </summary>
    public class TypePointRepository : GenericRepository<TypePointEntity>
    {
        #region Constructor

        public TypePointRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public int Add(TypePoint element)
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

        public TypePoint GetById(int id)
        {
            var typePoint = this.GetByIdPrivate(id);
            return typePoint != null ? typePoint.ToBo() : null;
        }

        private TypePointEntity GetByIdPrivate(int id)
        {
            return base.Where(x => x.Id == id).SingleOrDefault();
        }

        public void Update(TypePoint element)
        {
            var typePointToUpdate = this.GetByIdPrivate(element.Id);
            typePointToUpdate.Libelle = element.Libelle;            
            base.Update(typePointToUpdate);
        }

        public List<TypePoint> GetAllItems()
        {
            return base.GetAll().ToBos();
        }

        #endregion
    }    
}
