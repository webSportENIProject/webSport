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

        #region Competitor

        public static List<CompetitorModel> ToModels(this List<Competitor> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static CompetitorModel ToModel(this Competitor bo)
        {
            if (bo == null) return null;

            return new CompetitorModel
            {   
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                Race = bo.Race.ToModel(),
                DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
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

                Organisers = withJoin && bo.Organisers != null ? bo.Organisers.Select(x => x.ToModel()).ToList() : null,
                Competitors = withJoin && bo.Competitors != null ? bo.Competitors.Select(x => x.ToModel()).ToList() : null,
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
            };
        }

        #endregion

        #region DisplayConfiguration

        public static List<DisplayConfigurationModel> ToModels(this List<DisplayConfiguration> bos)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToModel()).ToList()
                : null;
        }

        public static DisplayConfigurationModel ToModel(this DisplayConfiguration bo)
        {
            if (bo == null) return null;

            return new DisplayConfigurationModel
            {
                Id = bo.Id,
                PersonneId = bo.PersonneId,
                DeviceName = bo.DeviceName,
                SpeedAvg = bo.SpeedAvg,
                SpeedMax = bo.SpeedMax,
                UnitDistance = bo.UnitDistance.ToModel(),

                Person = bo.Person.ToModel()
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
                Phone = bo.Phone,

                //DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
            };
        }

        public static OrganizerModel ToModel(this Organizer bo)
        {
            if (bo == null) return null;

            return new OrganizerModel
            {
                Id = bo.Id,
                Nom = bo.Nom,
                Prenom = bo.Prenom,
                DateNaissance = bo.DateNaissance,
                Email = bo.Email,
                Phone = bo.Phone,
                DisplayConfigurations = bo.DisplayConfigurations.Select(x => x.ToModel()).ToList()
            };
        }

        public static PoiModel ToModel(this Poi bo)
        {
            if (bo == null) return null;

            return new PoiModel
            {
                Id = bo.Id,
                GpsCoordinates = new CoordGpsModel
                {
                    Accuracy = bo.Accuracy,
                    Altitude = bo.Altitude,
                    AltitudeAccuracy = bo.AltitudeAccuracy,
                    Latitude = bo.Latitude,
                    Longitude = bo.Longitude
                },
                Category = bo.Category.ToModel(),
                Heading = bo.Heading,
                Speed = bo.Speed,
                Timestamp = bo.Timestamp,
                Title = bo.Title
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
    }
}