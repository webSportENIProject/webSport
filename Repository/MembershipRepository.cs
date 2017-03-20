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
    public class MembershipRepository : GenericRepository<webpages_Membership>
    {
        #region Constructor

        public MembershipRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        private webpages_Membership GetByIdPrivate(int id)
        {
            return base.Where(x => x.UserId == id).SingleOrDefault();
        }

        //DELETE
        public void Remove(int id)
        {
            var resultatToDelete = this.GetByIdPrivate(id);
            base.Remove(resultatToDelete);
        }
    }
}
