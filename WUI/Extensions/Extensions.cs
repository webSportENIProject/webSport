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

        #region Personne
        public static PersonneModel ToModel(this Personne bo)
        {
            if (bo == null) return null;
            String distance = "";
            if (bo.kms) {
                distance = "Kms";
            }
            else if(bo.miles){
                distance = "Miles";
            }

            return new PersonneModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                distance = distance
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

            bool kms = false;
            bool miles = false;

            if (model.distance.Equals("Kms"))
            {
                kms = true;
            }
            else if (model.distance.Equals("Miles"))
            {
                miles = true;
            }

            return new Personne
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                Phone = model.Phone,
                miles = miles,
                kms = kms,
                DateNaissance = (DateTime)model.DateNaissance
            };
        }
        #endregion

        #region Mail
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
        #endregion

        #region TypePoint       

        public static List<TypePointModel> ToModels(this List<TypePoint> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static TypePointModel ToModel(this TypePoint bo)
        {
            if (bo == null) return null;

            return new TypePointModel
            {
                Id = bo.Id,
                Libelle = bo.Libelle,
            };
        }

        public static TypePoint ToBo(this TypePointModel model)
        {
            if (model == null) return null;

            return new TypePoint
            {
                Id = model.Id,
                Libelle = model.Libelle,
            };
        }

        #endregion

        #region Point
        public static List<PointModel> ToModels(this List<Point> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static PointModel ToModel(this Point bo)
        {
            if (bo == null) return null;

            return new PointModel
            {
                Id = bo.Id,
                Titre = bo.Titre,
                Ordre = bo.Ordre,
                Longitude = bo.Longitude,
                Latitude = bo.Latitude,
                LibelleCourse = bo.Course.Title,
                LibelleTypePoint = bo.TypePoint.Libelle,
                IdTypePoint = bo.TypePoint.Id,
                IdCourse = bo.Course.Id
            };
        }

        public static Point ToBo(this PointModel model)
        {
            if (model == null) return null;

            return new Point
            {
                Id = model.Id,
                Titre = model.Titre,
                Ordre = model.Ordre,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                TypePointId = model.IdTypePoint,
                CourseId = model.IdCourse
            };
        }

        #endregion


    }
}