using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WUI.Models;

namespace WUI.Extensions
{
    public static class Extensions
    {
        #region Category

        public static List<CategoryModel> ToModels(this List<Category> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static CategoryModel ToModel(this Category bo)
        {
            if (bo == null) return null;

            return new CategoryModel
            {
                Id = bo.Id,
                Title = bo.Title
            };
        }

        #endregion

        #region Participant

        public static List<ParticipantModel> ToModels(this List<Participant> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static ParticipantModel ToModel(this Participant bo)
        {
            if (bo == null) return null;

            return new ParticipantModel
            {   
                PersonneId = bo.IdPersonne,
                CourseId = bo.IdCourse,
                EstCompetiteur = bo.EstCompetiteur,
                EstOrganisateur = bo.EstOrganisateur
            };
        }

        #endregion

        #region Race

        public static List<RaceModel> ToModels(this List<Race> bos, bool withJoin = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel(withJoin)).ToList()
                : null;
        }

        public static RaceModel ToModel(this Race bo, bool withJoin = false)
        {
            if (bo == null) return null;

            return new RaceModel
            {
                Id = bo.Id,
                Title = bo.Title,
                Description = bo.Description,
                DateStart = bo.DateStart,
                DateEnd = bo.DateEnd,
                Town = bo.Town,
                MaxParticipants = bo.MaxParticipants,

                Participants = withJoin && bo.Participants != null ? bo.Participants.Select(x => x.ToModel()).ToList() : null,
                //Pois = bo.Pois.Select(x => x.ToModel()).ToList(),
            };
        }

        public static Race ToBo(this RaceModel model)
        {
            if (model == null) return null;

            return new Race
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DateStart = model.DateStart,
                DateEnd = model.DateEnd,
                Town = model.Town,
                MaxParticipants = model.MaxParticipants
            };
        }

        #endregion


        public static PersonneModel ToModel(this Personne bo)
        {
            if (bo == null) return null;

            return new PersonneModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone
            };
        }        

        public static UnitDistanceModel ToModel(this UnitDistance bo)
        {
            UnitDistanceModel result;

            switch (bo)
            {
                case UnitDistance.Miles:
                    result = UnitDistanceModel.Miles;
                    break;

                default:
                case UnitDistance.Kilometers:
                    result = UnitDistanceModel.Kilometers;
                    break;
            }

            return result;
        }

        public static Personne ToBo(this PersonneModel model)
        {
            if (model == null) return null;

            return new Personne
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                Phone = model.Phone,
                DateNaissance = (DateTime)model.DateNaissance
            };
        }

        public static Mail ToBo(this MailModel model)
        {
            if (model == null) return null;

            return new Mail
            {
                Id = model.Id,
                Nom = model.Nom,
                Email = model.Email,
                Titre = model.Titre,
                Message = model.Message
            };
        }

    }
}