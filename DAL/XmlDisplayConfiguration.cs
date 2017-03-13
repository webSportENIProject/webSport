using BO;
using DAL.Serialisation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml.Serialization;


namespace DAL
{
    public class XmlDisplayConfiguration
    {
        public string CheminXml
        {
            get
            {
                return "~/App_Data/Xml/DisplayConf-{0}.xml";
            }
        }

        public string CheminJson
        {
            get
            {
                return "~/App_Data/Json/DisplayConf-{0}.json";
            }
        }

        public string CheminDat
        {
            get
            {
                return "~/App_Data/Autres/DisplayConf-{0}.dat";
            }
        }

        public string CheminSoap
        {
            get
            {
                return "~/App_Data/Autres/DisplayConf-{0}.soap";
            }
        }

        #region Sérialisation XML

        public void SerialiserXML(int personId, List<DisplayConfiguration> displayConf)
        {
            XmlSerialiser.SerialiserXML<List<DisplayConfiguration>>(
                displayConf,
                HttpContext.Current.Server.MapPath(string.Format(this.CheminXml, personId))
            );
        }

        public List<DisplayConfiguration> DeserialiserXML(int personId)
        {
            return XmlSerialiser.DeserialiserXML<List<DisplayConfiguration>>(
                HttpContext.Current.Server.MapPath(string.Format(this.CheminXml, personId))
            );
        }

        #endregion

        #region Sérialisation JSON

        public bool SerialiserJSON(int personId, List<DisplayConfiguration> displayConf)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<DisplayConfiguration>));
                using (FileStream stream = File.Create(HttpContext.Current.Server.MapPath(string.Format(this.CheminJson, personId))))
                {
                    js.WriteObject(stream, displayConf);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public List<DisplayConfiguration> DeserialiserJSON(int personId)
        {
            List<DisplayConfiguration> displayConf;

            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<DisplayConfiguration>));
                using (FileStream stream = File.OpenRead(HttpContext.Current.Server.MapPath(string.Format(this.CheminJson, personId))))
                {
                    displayConf = (List<DisplayConfiguration>)js.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la désérialisation de DisplayConfiguration", ex);
            }


            return displayConf;
        }

        #endregion

        #region Sérialisation Binaire

        public void SerialiserBinaire<T>(int personId, T objet)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                var destination = HttpContext.Current.Server.MapPath(string.Format(this.CheminDat, personId));
                using (var fichierSauvegarde = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fichierSauvegarde, objet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T DeserialiserBinaire<T>(int personId)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            T resultat;
            try
            {
                var source = HttpContext.Current.Server.MapPath(string.Format(this.CheminDat, personId));
                using (var fichierSauvegarde = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    resultat = (T)formatter.Deserialize(fichierSauvegarde);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resultat;
        }

        #endregion

        #region Sérialisation SOAP

        public void SerialiserSOAP<T>(int personId, T objet)
        {
            SoapFormatter formatter = new SoapFormatter();
            try
            {
                var destination = HttpContext.Current.Server.MapPath(string.Format(this.CheminSoap, personId));
                using (var fichierSauvegarde = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fichierSauvegarde, objet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T DeserialiserSOAP<T>(int personId)
        {
            SoapFormatter formatter = new SoapFormatter();
            T resultat;
            try
            {
                var source = HttpContext.Current.Server.MapPath(string.Format(this.CheminSoap, personId));
                using (var fichierSauvegarde = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    resultat = (T)formatter.Deserialize(fichierSauvegarde);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return resultat;
        }

        #endregion

        #region Sérialisation Fabrique

        public void SerialiserFactory<T>(int personId, T objet)
        {
            string destination;
            
            // En binary
            destination = HttpContext.Current.Server.MapPath(string.Format(this.CheminDat, personId));
            // En soap
            //destination = HttpContext.Current.Server.MapPath(string.Format(this.CheminSoap, personId));

            var factory = FabriqueSerialiseur.GetSerialiseur(destination);
            factory.Serialiser<T>(objet, destination);
        }

        public T DeserialiserFactory<T>(int personId)
        {
            string source;

            // En binary
            source = HttpContext.Current.Server.MapPath(string.Format(this.CheminDat, personId));
            // En soap
            //source = HttpContext.Current.Server.MapPath(string.Format(this.CheminSoap, personId));

            var factory = FabriqueSerialiseur.GetSerialiseur(source);
            return factory.Deserialiser<T>(source);
        }

        #endregion
    }
}
