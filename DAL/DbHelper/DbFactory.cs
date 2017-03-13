using BO;
using Npgsql;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;


namespace DAL
{
    /// <summary>
    /// Fabrique permettant de récupérer une instance de connexion à une base de données
    /// </summary>
    public static class DbFactory
    {
        public static DbConnection InitializeConnection()
        {
            DbConnection cnx = null;

            try
            {
                // Le choix s'effectue via un paramètre dans le fichier de config de l'application
                var wishedDbServer = ConfigurationManager.AppSettings["wishedDbServer"];
                string connectionStringName = string.Empty;
                if (wishedDbServer == Enums.GetDescription(DbServer.SQL))
                {
                    connectionStringName = "SqlAdoCs";
                }
                else if (wishedDbServer == Enums.GetDescription(DbServer.POSTGRESQL))
                {
                    connectionStringName = "PostGreSqlAdoCs";
                }
                else if (wishedDbServer == Enums.GetDescription(DbServer.ORACLE))
                {
                    connectionStringName = "OracleAdoCs";
                }

                // Au lieu de : cnx = new SqlConnection(); ou cnx = new OracleConnection(); , etc
                // On peut utiliser la fabrique proposée par la classe DbProviderFactories
                // On fait alors une Fabrique de fabrique
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringName];
                cnx = DbProviderFactories.GetFactory(settings.ProviderName).CreateConnection();
                cnx.ConnectionString = settings.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("DbFactory - Une erreur est survenue : {0}", ex.Message));
            }

            return cnx;
        }
    }
}
