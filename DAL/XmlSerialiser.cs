using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL
{
    public static class XmlSerialiser
    {
        public static void SerialiserXML<T>(T objet, String destination)
        {
            FileStream fichierSauvegarde = null;
            XmlSerializer _formatter = new XmlSerializer(typeof(T));
            try
            {
                fichierSauvegarde = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None);
                _formatter.Serialize(fichierSauvegarde, objet);
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("Une erreur est survenue lors de la sérialisation : {0}", e.Message));
            }
            finally
            {
                if (fichierSauvegarde != null)
                    fichierSauvegarde.Close();
            }
        }

        public static T DeserialiserXML<T>(string origine)
        {
            FileStream fichierSauvegarde = null;
            XmlSerializer _formatter = new XmlSerializer(typeof(T));
            T objet;
            try
            {
                using (fichierSauvegarde = new FileStream(origine, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    objet = (T)_formatter.Deserialize(fichierSauvegarde);
                }
                return objet;
            }
            catch (Exception e)
            {
                throw new ApplicationException(string.Format("Une erreur est survenue lors de la désérialisation : {0}", e.Message));
            }
        }
    }
}
