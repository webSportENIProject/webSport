using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Serialisation
{
    public class SerialiseurBinaire : Serialiseur
    {
        #region Singleton

        private static SerialiseurBinaire _instance = null;
        public static SerialiseurBinaire GetInstance()
        {
            if (_instance == null)
                _instance = new SerialiseurBinaire();
            return _instance;
        }

        private SerialiseurBinaire()
        {
            this._formatter = new BinaryFormatter();
        }

        #endregion
    }
}
