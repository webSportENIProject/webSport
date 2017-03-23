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
                element.kms = true;
                var result = base.Add(element.ToDataEntity());
                return result.Id;
            }
            catch
            {
                return 0;
            }
        }

        //GET ALL
        public List<Personne> GetAllItems()
        {
            return base.GetAll().ToBos();
        }

        public List<Personne> GetAllItemsByLimit(int skip, int take)
        {
            return this.context.Personne.OrderBy(x => x.Id).Skip(skip).Take(take).ToList().ToBos();
        }

        public int Count()
        {
            return this.context.Personne.Count();
        }

        public Personne getPersonneByUserId(int id) {
            return base.Where(x => x.Id == id).SingleOrDefault().ToBO();
        }

        public Personne GetByIdUserTable(int userId)
        {
            return base.Where(x => x.UserTableId == userId).SingleOrDefault().ToBO();
        }

        public void Update(Personne element)
        {
            PersonneEntity personneToUpdate = base.Where(x => x.Id == element.Id).SingleOrDefault();
            personneToUpdate.Nom = element.Nom;
            personneToUpdate.Prenom = element.Prenom;
            personneToUpdate.Email = element.Email;
            personneToUpdate.Telephone = element.Phone;
            personneToUpdate.DateNaissance = element.DateNaissance;
            personneToUpdate.kms = element.kms;
            personneToUpdate.miles = element.miles;
            base.Update(personneToUpdate);
        }

        public void RemoveByIdUserTable(int userId) {
            PersonneEntity entity = base.Where(x => x.UserTableId == userId).SingleOrDefault();
            base.Remove(entity);
        }

        #endregion
    }
}
