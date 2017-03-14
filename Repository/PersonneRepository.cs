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
    public class PersonneRepository : GenericRepository<PersonneEntity>
    {
        #region Constructor

        public PersonneRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public int Add(Personne element)
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

        #endregion
    }
}
