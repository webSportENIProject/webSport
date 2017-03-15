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
    public class MailRepository : GenericRepository<MailEntity>
    {
        public MailRepository(WebSportEntities context)
            : base(context)
        {
        }

        public int Add(Mail element)
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
    }
}
