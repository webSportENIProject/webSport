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

        public static RaceModel ToModel(this Race bo, bool withJoinParticipants = false, bool withJoinPoints = false)
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

                Participants = withJoinParticipants && bo.Participants != null ? bo.Participants.Select(x => x.ToModel()).ToList() : null,
                Points = withJoinPoints && bo.Points != null ? bo.Points.Select(x => x.ToModel()).ToList() : null
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
        public static List<PersonneModel> ToModels(this List<Personne> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }
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

        #region Contact

        public static List<ContactModel> ToModels(this List<Mail> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static ContactModel ToModel(this Mail bo)
        {
            if (bo == null) return null;

            return new ContactModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Email = bo.Email,
                Titre = bo.Titre,
                Message = bo.Message
            };
        }

        public static Mail ToBo(this ContactModel model)
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
            PointModel model = new PointModel
            {
                Id = bo.Id,
                Titre = bo.Titre,
                Ordre = bo.Ordre,
                Longitude = bo.Longitude,
                Latitude = bo.Latitude,
                IdTypePoint = bo.TypePointId,
                IdCourse = bo.CourseId
            };
            if (bo.Course != null)
            {
                model.LibelleCourse = bo.Course.Title;
            }
            if (bo.TypePoint != null)
            {
                model.LibelleTypePoint = bo.TypePoint.Libelle;
            }
            
            return model;
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

        #region Participant
        public static ResultatModel ToModel(this Resultat bo)
        {
            if (bo == null) return null;

            return new ResultatModel
            {
                id = bo.id,
                idCourse = bo.idCourse,
                idPersonne = bo.idPersonne,
                idPoint = bo.idPoint,
                temps = bo.temps
            };
        }

        public static List<ResultatModel> ToModels(this List<Resultat> bos)
        {
            List<ResultatModel> models = new List<ResultatModel>();

            foreach (Resultat bo in bos) {
                models.Add(bo.ToModel());
            }

            return models;
        }
        #endregion
    }
}