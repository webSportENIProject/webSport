using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
        where T : class
    {
        T Add(T element);
        
        List<T> GetAll();
        List<T> Where(Expression<Func<T, bool>> conditionWhere);

        void Update(T element);

        void Remove(T element);
    }
}
