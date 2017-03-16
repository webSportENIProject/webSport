using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using BO;

namespace BLL
{
    public class MailUtil
    {
        private MailMessage mail = new MailMessage();

        private SmtpClient initMail() {
            mail.From = new System.Net.Mail.MailAddress("websporteni@gmail.com");

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 465;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("websporteni@gmail.com", "Websport44");
            smtp.Host = "smtp.gmail.com";

            return smtp;
        }

        public void sendMailInscription(int idRace, int idUser) {
            SmtpClient smtp = initMail();
            Race course = MgtRace.GetInstance().GetRace(idRace);
            Personne personne = MgtPersonne.GetInstance().GetPersonneByIdUserTable(idUser);

            mail.To.Add(new MailAddress(personne.Email));

            mail.IsBodyHtml = true;
            mail.Subject = "WebSport : Inscription à la course " + course.Title;

            string st = "Confirmation de votre inscription à la course " + course.Title
                + " qui aura le lieu le " + course.DateStart + " à " + course.Town;

            mail.Body = st;
            smtp.Send(mail);
        }
    }
}