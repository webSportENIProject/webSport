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
    public class ParticipantRepository : GenericRepository<ParticipantEntity>
    {
        #region Constructor

        public ParticipantRepository(WebSportEntities context)
            : base(context)
        {
        }

        #endregion

        #region Public methods

        public Boolean AddCompetiteur(int idRace, int idPersonne)
        {
            ParticipantEntity entity = new ParticipantEntity()
            {
                PersonneId = idPersonne,
                CourseId = idRace,
                dateInscription = DateTime.Today,
                EstCompetiteur = true,
                EstOrganisateur = false
            };

            try
            {
                var result = base.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //GET ALL
        public List<Participant> GetAllItems()
        {
            return base.GetAll().ToParticipantBos();
        }

        //GET BY ID PERSONNE
        public List<Participant> GetAllItemsByIdPersonne(int id)
        {
            return base.Where(x => x.PersonneId == id).ToList().ToParticipantBos();
        }

        //GET BY ID RACE
        public List<Participant> GetAllItemsByIdRace(int idRace)
        {
            return base.Where(x => x.CourseId == idRace).ToList().ToParticipantBos();
        }

        //REMOVE
        public void Remove(int idPersonne, int idRace)
        {
            var participantToDelete = base.Where(x => x.PersonneId == idPersonne && x.CourseId == idRace).SingleOrDefault();
            base.Remove(participantToDelete);
        }

        public void RemoveAllParticipationByCourse(int idCourse) {
            List<ParticipantEntity> entities = base.Where(x => x.CourseId == idCourse).ToList();
            foreach (ParticipantEntity entity in entities) {
                base.Remove(entity);
            }
        }

        public void RemoveAllParticipationByIdPersonne(int idPersonne)
        {
            List<ParticipantEntity> entities = base.Where(x => x.PersonneId == idPersonne).ToList();
            foreach (ParticipantEntity entity in entities)
            {
                base.Remove(entity);
            }
        }


        //GET BY ID RACE
        public bool isInscrit(int idRace, int idPersonne)
        {
            return base.Where(x => x.CourseId == idRace).Where(x => x.PersonneId == idPersonne).Count() > 0;
        }

        #endregion
    }
}
