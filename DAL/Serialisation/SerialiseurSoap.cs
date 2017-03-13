using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Serialisation
{
    public class SerialiseurSoap : Serialiseur
    {
        #region Singleton

        private static SerialiseurSoap _instance = null;
        public static SerialiseurSoap GetInstance()
        {
            if (_instance == null)
                _instance = new SerialiseurSoap();
            return _instance;
        }

        private SerialiseurSoap()
        {
            this._formatter = new SoapFormatter();
        }

        #endregion
    }
}
