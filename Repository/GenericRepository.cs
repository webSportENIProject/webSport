using DAL.EntityFramework;
using DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    /// <summary>
    /// Repository de base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        #region Properties

        /// <summary>
        /// Context
        /// </summary>
        protected WebSportEntities context;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor by default
        /// </summary>
        /// <param name="context">The context</param>
        public GenericRepository(WebSportEntities context)
        {
            this.context = context;
        }

        #endregion

        #region Public methods

        public virtual T Add(T element)
        {
            context.Set<T>().Add(element);
            context.SaveChanges();

            return element;
        }

        public virtual List<T> Where(Expression<Func<T, bool>> conditionWhere)
        {
            return context.Set<T>().Where(conditionWhere).ToList();
        }

        public virtual List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public virtual void Update(T element)
        {
            context.Entry(element).State = EntityState.Modified;
        }

        public virtual void Remove(T element)
        {
            context.Entry(element).State = EntityState.Deleted;
        }

        #endregion
    }
}
