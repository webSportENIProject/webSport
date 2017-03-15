﻿using BO;
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

        #endregion
    }
}