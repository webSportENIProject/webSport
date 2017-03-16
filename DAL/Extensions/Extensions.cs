﻿using BO;
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
                MaxParticipants = bo.MaxParticipants,

                Participants = withJoin && bo.Participant != null ? bo.Participant.Where(x => x.EstCompetiteur).Select(x => x.ToParticipantBo()).ToList() : null
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
                MaxParticipants = model.MaxParticipants,
            };
        }      

        #endregion

        #region Participant

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

        #region Personne
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
        #endregion

        #region Mail
        public static MailEntity ToDataEntity(this BO.Mail model)
        {
            if (model == null) return null;

            return new MailEntity
            {
                Id = model.Id,
                Nom = model.Nom,
                Email = model.Email,
                Titre = model.Titre,
                Message = model.Message
            };
        }

        public static Mail ToBO(this MailEntity entity)
        {
            if (entity == null) return null;

            return new Mail
            {
                Id = entity.Id,
                Nom = entity.Nom,
                Email = entity.Email,
                Titre = entity.Titre,
                Message = entity.Message
            };
        }
        #endregion

        #region Point

        public static List<Point> ToBos(this List<PointEntity> bos, bool withJoinTypeCourse = false, bool withJoinRace = false)
        {
            return bos != null
                ? bos.Where(x => x != null).Select(x => x.ToBo(withJoinTypeCourse, withJoinRace)).ToList()
                : null;
        }

        public static Point ToBo(this PointEntity bo, bool withJoinTypeCourse = false, bool withJoinRace = false)
        {
            if (bo == null) return null;

            return new Point
            {
                Id = bo.Id,
                Titre = bo.Titre,
                Ordre = bo.Ordre,
                Longitude = bo.Longitude,
                Latitude = bo.Latitude
            };
        }

        public static PointEntity ToDataEntity(this Point model)
        {
            if (model == null) return null;

            return new PointEntity
            {
                Id = model.Id,
                Titre = model.Titre,
                Ordre = model.Ordre,
                Longitude = model.Longitude,
                Latitude = model.Latitude
            };
        }

        #endregion

        #region TypePoint
        public static TypePoint ToBo(this TypePointEntity bo)
        {
            if (bo == null) return null;

            return new TypePoint
            {
                Id = bo.Id,
                Libelle = bo.Libelle
            };
        }

        public static TypePointEntity ToDataEntity(this TypePoint model)
        {
            if (model == null) return null;

            return new TypePointEntity
            {
                Id = model.Id,
                Libelle = model.Libelle
            };
        }

        #endregion


    }
}
