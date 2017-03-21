using BO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Extensions;

namespace BLL
{
    public class MgtMail
    {
        private static MgtMail _instance;

        public static MgtMail GetInstance()
        {
            if (_instance == null)
                _instance = new MgtMail();
            return _instance;
        }

        private UnitOfWork _uow { get; set; }

        private MgtMail()
        {
            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = UnitOfWork.GetInstance();
        }

        public bool AddMail(Mail mail)
        {
            if (mail != null)
            {

                int lastId = this._uow.MailRepo.Add(mail);
                if (lastId > 0)
                {
                    mail.Id = lastId;
                }
                return true;
            }

            return false;
        }

        public List<Mail> GetAllContact()
        {
            List<Mail> contact = this._uow.MailRepo.GetAllItems();
            return contact;
        }
    }
}
