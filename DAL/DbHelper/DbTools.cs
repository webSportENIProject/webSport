using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbTools : IDisposable
    {
        #region Propriétés

        private DbConnection _cnx;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        public DbTools()
        {
            SeConnecter();
        }

        #endregion

        #region Méthodes publiques

        public DbConnection SeConnecter()
        {
            try
            {
                if (_cnx == null
                    || _cnx.State == ConnectionState.Closed
                    || _cnx.State == ConnectionState.Broken)
                {
                    _cnx = DbFactory.InitializeConnection();
                    _cnx.Open();
                }

                return _cnx;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("DbTools - Une erreur est survenue à la connexion à la base de données : {0}", ex.Message));
            }
        }

        public void SeDeconnecter()
        {
            try
            {
                if (_cnx != null
                    && _cnx.State != ConnectionState.Closed)
                {
                    _cnx.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("DbTools - Une erreur est survenue à la fermeture de la connexion : {0}", ex.Message));
            }
        }

        public DbCommand CreerRequete(string requete)
        {
            DbCommand cmd = _cnx.CreateCommand();
            cmd.CommandText = requete;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public DbCommand CreerProcedureStockee(string requete)
        {
            DbCommand cmd = _cnx.CreateCommand();
            cmd.CommandText = requete;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public void CreerParametre(DbCommand cmd,
                                    string nomParametre,
                                    object valeur,
                                    ParameterDirection direction = ParameterDirection.Input,
                                    DbType type = DbType.String,
                                    int taille = 0)
        {
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = nomParametre;
            // Attention au valeur nulle issue des objets
            param.Value = valeur ?? DBNull.Value;
            param.DbType = type;
            param.Direction = direction;
            if (taille > 0)
            {
                param.Size = taille;
            }
            cmd.Parameters.Add(param);
        }

        public DbTransaction CreerTransaction()
        {
            return _cnx.BeginTransaction();
        }

        #endregion

        #region Méthodes implémentées pour IDisposable

        public void Dispose()
        {
            SeDeconnecter();
        }

        #endregion
    }
}