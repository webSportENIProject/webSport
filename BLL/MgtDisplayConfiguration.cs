using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class MgtDisplayConfiguration
    {
        private List<DisplayConfiguration> _displayConf;

        public MgtDisplayConfiguration()
        {
            // TODO : Aller chercher ces informations en BDD via la DAL

            _displayConf = new List<DisplayConfiguration>
            {
                new DisplayConfiguration
                {
                    Id = new Guid(),
                    PersonneId = 1,
                    Person = new MgtCompetitor().GetCompetitor(1),
                    SpeedAvg = true,
                    SpeedMax = false,
                    UnitDistance = UnitDistance.Kilometers,
                    DeviceName = "Android X5S"
                },
                new DisplayConfiguration
                {
                    Id = new Guid(),
                    PersonneId = 1,
                    Person = new MgtCompetitor().GetCompetitor(1),
                    SpeedAvg = true,
                    SpeedMax = true,
                    UnitDistance = UnitDistance.Miles,
                    DeviceName = "iOS 7"
                },
                new DisplayConfiguration
                {
                    Id = new Guid(),
                    PersonneId = 2,
                    Person = new MgtCompetitor().GetCompetitor(2),
                    SpeedAvg = false,
                    SpeedMax = true,
                    UnitDistance = UnitDistance.Kilometers,
                    DeviceName = "Lumia 620"
                },
            };
        }


        public List<DisplayConfiguration> GetDisplayConfiguration()
        {
            return _displayConf;
        }

        public DisplayConfiguration GetDisplayConfiguration(Guid id)
        {
            return _displayConf.Find(x => x.Id == id);
        }

        public List<DisplayConfiguration> GetDisplayConfiguration(int id)
        {
            var service = new XmlDisplayConfiguration();

            var confFromPerson = _displayConf.Where(x => x.PersonneId == id).ToList();

            // Sérial/Déserial en XML
            service.SerialiserXML(id, confFromPerson);
            var xmlReturnedList = service.DeserialiserXML(id);

            //// Sérial/Déserial en JSON
            //service.SerialiserJSON(id, confFromPerson);
            //var jsonReturnedList = service.DeserialiserJSON(id);

            //// Sérial/Déserial en binary
            //service.SerialiserBinaire(id, confFromPerson);
            //var binaryReturnedList = service.DeserialiserBinaire<List<DisplayConfiguration>>(id);

            //// Sérial/Déserial en SOAP
            //service.SerialiserSOAP(id, confFromPerson.ToArray());
            //var soapReturnedList = service.DeserialiserSOAP<DisplayConfiguration[]>(id).ToList();

            //// Sérial/Déserial avec la fabrique
            //service.SerialiserFactory(id, confFromPerson.ToArray());
            //service.DeserialiserFactory<DisplayConfiguration[]>(id);

            return xmlReturnedList;
        }
    }
}
