using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Serialisation
{
    /// <summary>
    /// Classe unique permettant de sérialiser en fonction de l'extension du fichier souhaité
    /// </summary>
    /// <remarks>
    /// Pour les .DAT => on utilisera BinaryFormatter
    /// Pour les .SOAP => on utilisera SoapFormatter
    /// </remarks>
    public class FabriqueSerialiseur
    {
        public static Serialiseur GetSerialiseur(string destination)
        {
            string extension = Path.GetExtension(destination);

            Serialiseur serialiseur;
            if (".dat".Equals(extension))
            {
                serialiseur = SerialiseurBinaire.GetInstance();
            }
            else if (".soap".Equals(extension))
            {
                serialiseur = SerialiseurSoap.GetInstance();
            }
            else
            {
                throw new ApplicationException(string.Format("Le format {0} n'est pas géré", extension));
            }

            return serialiseur;
        }
    }
}
