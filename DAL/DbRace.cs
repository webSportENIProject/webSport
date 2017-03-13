using BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EntityFramework;
using DAL.Extensions;

namespace DAL
{
    public class DbRace
    {
        #region Singleton

        private static DbRace _instance;
        public static DbRace GetInstance()
        {
            if (_instance == null)
                _instance = new DbRace();
            return _instance;
        }

        #endregion

        #region Requêtes const

        private const string RQT_GET_RACE = "SELECT Pct.EstCompetiteur as PctEstCompetiteur, Pct.EstOrganisateur as PctEstOrganisateur " +
                                            ", P.Id as PId, P.Nom as PNom, P.Prenom as PPrenom, P.Email as PEmail, P.Telephone as PTelephone, P.DateNaissance as PDateNaissance " +
                                            ", C.Id as CId, C.Titre as CTitre, C.Description as CDescription, C.DateStart as CDateStart, C.DateEnd as CDateEnd, C.Ville as CVille " +
                                            "FROM Participant Pct " +
                                            "INNER JOIN Personne P ON Pct.PersonneId = P.Id " +
                                            "INNER JOIN Course C ON Pct.CourseId = C.Id";

        private const string RQT_GET_RACE_PS = "GetRaceById";

        private const string RQT_ADD_RACE = "INSERT INTO Course VALUES (@title, @description, @datestart, @dateend, @ville)";

        private const string RQT_GET_LAST_ADDED = "SELECT IDENT_CURRENT('Course')";
        
        #endregion

        #region Public methods

        public int AddRace(Race race)
        {
            if (race == null) return 0;

            int retour = 0;

            // Utilisation d'Entity Framework => On ne nécessite plus de transactions
            using (var context = new WebSportEntities())
            {
                var newRace = new RaceEntity()
                {
                    Title = race.Title,
                    Description = race.Description,
                    DateStart = race.DateStart,
                    DateEnd = race.DateEnd,
                    Town = race.Town,
                };

                context.RaceEntities.Add(newRace);
                context.SaveChanges();
                retour = newRace.Id;
            }

            // Utilisation de transactions avec Entity Framework
            //using (var contextBis = new WebSportEntities())
            //{
            //    using (var transac = contextBis.Database.Connection.BeginTransaction())
            //    {
            //        try
            //        {
            //             DO QUERY

            //            contextBis.SaveChanges();
            //            transac.Commit();
            //        }
            //        catch (Exception)
            //        {
            //            transac.Rollback();
            //        }
            //    }
            //}

            // Si on utilise ADO.NET en direct, on utiliserait le code ci-dessous
            //try
            //{
            //    var instance = new DbTools();

            //    // Préparation d'une transaction
            //    var transac = instance.CreerTransaction();

            //    // Exécution de la requête d'ajout + on l'inclut dans la transaction
            //    var commandAdd = instance.CreerRequete(RQT_ADD_RACE);
            //    instance.CreerParametre(commandAdd, "@title", race.Title);
            //    instance.CreerParametre(commandAdd, "@description", race.Description);
            //    instance.CreerParametre(commandAdd, "@datestart", race.DateStart);
            //    instance.CreerParametre(commandAdd, "@dateend", race.DateEnd);
            //    instance.CreerParametre(commandAdd, "@ville", race.Town);
            //    commandAdd.Transaction = transac;
            //    commandAdd.ExecuteNonQuery();

            //    // Exécution de la requête de recupération du dernier id ajouté + on l'inclut dans la transaction
            //    var commandSelect = instance.CreerRequete(RQT_GET_LAST_ADDED);
            //    commandSelect.Transaction = transac;

            //    retour = Convert.ToInt32(commandSelect.ExecuteScalar());
            //    if (retour > 0)
            //    {
            //        transac.Commit();
            //    }
            //    else
            //    {
            //        transac.Rollback();
            //    }
            //}
            //catch (Exception)
            //{
            //    retour = 0;
            //}

            return retour;
        }

        public List<Race> GetRace()
        {
            List<Race> list = new List<Race>();

            // Utilisation d'Entity Framework
            using (var context = new WebSportEntities())
            {
                return context.RaceEntities.ToList().Select(x => x.ToBo()).ToList();
            }

            // Si on utilise ADO.NET en direct, on utiliserait le code ci-dessous
            //var instance = new DbTools();
            //var command = instance.CreerRequete(RQT_GET_RACE);

            //DbDataReader reader = command.ExecuteReader();
            //return this.BuildRaceList(reader);
        }

        public Race GetRace(int id)
        {
            // Utilisation d'Entity Framework
            using (var context = new WebSportPsEntities())
            {
                return context.GetRaceById(id).First().ToBo();
            }

            // Si on utilise ADO.NET en direct, on utiliserait le code ci-dessous
            //var instance = new DbTools();
            //var command = instance.CreerProcedureStockee(RQT_GET_RACE_PS);
            //instance.CreerParametre(command, "@id", id, ParameterDirection.Input, DbType.Int32);

            //DbDataReader reader = command.ExecuteReader();
            //return this.BuildRace(reader);
        }

        public bool UpdateRace(Race race)
        {
            // Utilisation d'Entity Framework
            using (var context = new WebSportEntities())
            {
                var initialRace = context.RaceEntities.SingleOrDefault(x => x.Id == race.Id);
                if (initialRace != null)
                {
                    initialRace.Title = race.Title;
                    initialRace.Description = race.Description;
                    initialRace.DateStart = race.DateStart;
                    initialRace.DateEnd = race.DateEnd;
                    initialRace.Town = race.Town;
                }
                else
                {
                    return false;
                }

                context.SaveChanges();
                return true;
            }
        }

        public bool RemoveRace(int raceId)
        {
            // Utilisation d'Entity Framework
            using (var context = new WebSportEntities())
            {
                var raceToDelete = context.RaceEntities.SingleOrDefault(x => x.Id == raceId);
                if (raceToDelete != null)
                {
                    context.RaceEntities.Remove(raceToDelete);
                }
                else
                {
                    return false;
                }

                context.SaveChanges();
                return true;
            }
        }

        #endregion

        #region Private methods

        private List<Race> BuildRaceList (DbDataReader reader)
        {
            List<Race> list = new List<Race>();

            while (reader.Read())
            {
                var raceId = reader.GetInt32(reader.GetOrdinal("CId"));

                Race r;
                if (list.All(x => x.Id != raceId))
                {
                    r = new Race
                    {
                        Id = raceId,
                        Title = reader.GetString(reader.GetOrdinal("CTitre")),
                        Description = reader.GetString(reader.GetOrdinal("CDescription")),
                        DateStart = reader.GetDateTime(reader.GetOrdinal("CDateStart")),
                        DateEnd = reader.GetDateTime(reader.GetOrdinal("CDateEnd")),
                        Town = reader.GetString(reader.GetOrdinal("CVille")),
                        Competitors = new List<Competitor>(),
                        Organisers = new List<Organizer>()
                    };
                    list.Add(r);
                }
                else
                {
                    r = list.Single(x => x.Id == raceId);
                }

                // Récupération du type de participans
                var isCompetitor = reader.GetBoolean(reader.GetOrdinal("PctEstCompetiteur"));
                var isOrganiser = reader.GetBoolean(reader.GetOrdinal("PctEstOrganisateur"));
                if (isCompetitor)
                {
                    Competitor c = new Competitor
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("PId")),
                        Nom = reader.GetString(reader.GetOrdinal("PNom")),
                        Prenom = reader.GetString(reader.GetOrdinal("PPrenom")),
                        Email = reader.GetString(reader.GetOrdinal("PEmail")),
                        Phone = reader.GetString(reader.GetOrdinal("PTelephone")),
                        DateNaissance = reader.GetDateTime(reader.GetOrdinal("PDateNaissance")),
                        Race = r
                    };
                    r.Competitors.Add(c);
                }

                if (isOrganiser)
                {
                    Organizer o = new Organizer
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("PId")),
                        Nom = reader.GetString(reader.GetOrdinal("PNom")),
                        Prenom = reader.GetString(reader.GetOrdinal("PPrenom")),
                        Email = reader.GetString(reader.GetOrdinal("PEmail")),
                        Phone = reader.GetString(reader.GetOrdinal("PTelephone")),
                        DateNaissance = reader.GetDateTime(reader.GetOrdinal("PDateNaissance"))
                    };
                    r.Organisers.Add(o);
                }
            }

            return list;
        }

        private Race BuildRace(DbDataReader reader)
        {
            // On lit la première ligne
            var result = reader.Read();

            Race r = null;
            if (result)
            {
                // On construit l'objet
                r = new Race
                {
                    Id = reader.GetInt32(reader.GetOrdinal("CId")),
                    Title = reader.GetString(reader.GetOrdinal("CTitre")),
                    Description = reader.GetString(reader.GetOrdinal("CDescription")),
                    DateStart = reader.GetDateTime(reader.GetOrdinal("CDateStart")),
                    DateEnd = reader.GetDateTime(reader.GetOrdinal("CDateEnd")),
                    Town = reader.GetString(reader.GetOrdinal("CVille"))
                };
            }

            return r;
        }

        #endregion
    }
}
