using BO;
using DAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Extensions
{
    public static class Extensions
    {
        #region Race

        public static List<Race> ToBos(this List<CourseEntity> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoin)).ToList()
                : null;
        }

        public static Race ToBo(this CourseEntity bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new Race
            {
                Id = bo.Id,
                Title = bo.Titre,
                Description = bo.Description,
                DateStart = bo.DateStart,
                DateEnd = bo.DateEnd,
                Town = bo.Ville,

                Organisers = withJoin && bo.Participant != null ? bo.Participant.Where(x => x.EstOrganisateur).Select(x => x.ToOrganiserBo()).ToList() : null,
                Competitors = withJoin && bo.Participant != null ? bo.Participant.Where(x => x.EstCompetiteur).Select(x => x.ToCompetitorBo()).ToList() : null
            };
        }

        public static CourseEntity ToDataEntity(this Race model)
        {
            if (model == null) return null;

            return new CourseEntity
            {
                Id = model.Id,
                Titre = model.Title,
                Description = model.Description,
                DateStart = model.DateStart,
                DateEnd = model.DateEnd,
                Ville = model.Town,
            };
        }


        public static Race ToBo(this GetRaceById_Result entity)
        {
            if (entity == null) return null;

            return new Race
            {
                Id = entity.CId,
                Title = entity.CTitre,
                Description = entity.CDescription,
                DateStart = entity.CDateStart,
                DateEnd = entity.CDateEnd,
                Town = entity.CVille
            };
        }

        #endregion

        #region Competitor

        public static List<Participant> ToParticipantBos(this List<ParticipantEntity> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToParticipantBo()).ToList()
                : null;
        }

        public static Participant ToParticipantBo(this ParticipantEntity bo)
        {
            if (bo == null) return null;

            return new Participant
            {
                IdPersonne = bo.PersonneId,
                IdCourse = bo.CourseId,
                EstCompetiteur = bo.EstCompetiteur,
                EstOrganisateur = bo.EstOrganisateur
            };
        }

        #endregion

        #region Organizer

        public static List<Organizer> ToOrganiserBos(this List<ParticipantEntity> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToOrganiserBo()).ToList()
                : null;
        }

        public static Organizer ToOrganiserBo(this ParticipantEntity bo)
        {
            if (bo == null) return null;

            return new Organizer
            {
                Id = bo.PersonneId,
                Nom = bo.Personne.Nom,
                Prenom = bo.Personne.Prenom,
                DateNaissance = bo.Personne.DateNaissance.HasValue ? bo.Personne.DateNaissance.Value : DateTime.MinValue,
                Email = bo.Personne.Email,
                Phone = bo.Personne.Telephone
            };
        }

        #endregion

        public static PersonneEntity ToDataEntity(this BO.Personne model)
        {
            if (model == null) return null;

            return new PersonneEntity
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                Telephone = model.Phone,
                DateNaissance = model.DateNaissance,
                UserTableId = model.UserTable
            };
        }

        public static Personne ToBO(this PersonneEntity entity)
        {
            if (entity == null) return null;

            return new Personne
            {
                Id = entity.Id,
                Nom = entity.Nom,
                Prenom = entity.Prenom,
                Email = entity.Email,
                Phone = entity.Telephone,
                DateNaissance = (DateTime)entity.DateNaissance,
                UserTable = (int)entity.UserTableId
            };
        }
    }
}
