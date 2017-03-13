using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Serialiseur
    {
        protected IFormatter _formatter;

        public void Serialiser<T>(T obj, string destination)
        {
            try
            {
                using (var fichierSauvegarde = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    _formatter.Serialize(fichierSauvegarde, obj);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Une erreur est survenue: {0}", ex.Message));
            }
        }

        public T Deserialiser<T>(string source)
        {
            T objetDeserialisee = default(T);
            try
            {
                using (var fichierSauvegarde = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    objetDeserialisee = (T)_formatter.Deserialize(fichierSauvegarde);
                }
                return objetDeserialisee;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Une erreur est survenue: {0}", ex.Message));
            }
        }
    }
}
