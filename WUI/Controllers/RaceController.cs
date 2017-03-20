using BLL;
using WebMatrix.WebData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;
using WUI.Models;
using BO;

namespace WUI.Controllers
{
    // Pour appeler n'importe quelle méthode, l'utilisateur doit être connecté
    // Sauf si une méthode lève la condition avec : [AllowAnonymous]
    [Authorize]
    public class RaceController : Controller
    {        

        /// <summary>
        /// Constructeur
        /// </summary>
        public RaceController()
        {           
        }

        //
        // GET: /Race/
        // Tous les utilisateurs peuvent visionnés la liste
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<RaceModel> result = MgtRace.GetInstance().GetAllItemsWithParticipants().ToModels(true);
            int idUser = 0;
            if (WebSecurity.IsAuthenticated) {
                if (!WebSecurity.Initialized) {
                    WebSecurity.InitializeDatabaseConnection("SqlAdoCs", "UserTable", "Id", "Name", autoCreateTables: true);
                }
                idUser = WebSecurity.CurrentUserId;
            }
            if (idUser > 0) {
                Personne user = MgtPersonne.GetInstance().GetPersonneByIdUserTable(idUser);
                for (int i = 0; i < result.Count; i++) {
                    bool inscrit = MgtParticipant.GetInstance().isIncrit(result.ElementAt(i).Id, user.Id);
                    result.ElementAt(i).inscrit = inscrit;
                }
            }
            

            return View(result);
        }       

        //
        // GET: /Race/Create
        [Authorize(Roles="Administrateur")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Race/Create
        [HttpPost]
        // Le flag ci-dessus permet de préciser à l'action Create qu'il faut vérifier
        // si le token issu du cookie d'authentification a été bien été défini dans
        // la requête HTTP POST pour l'envoi du formulaire
        [ValidateAntiForgeryToken]
        public ActionResult Create(RaceModel race)
        {
            try
            {
                if (ModelState.IsValid && MgtRace.GetInstance().AddRace(race.ToBo()))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Race/Edit/5
        [Authorize(Roles = "Administrateur")]
        public ActionResult Edit(int id = 0)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        public ActionResult Register(int id = 0)
        {
            RaceModel model = MgtRace.GetInstance().GetRace(id).ToModel();
            if (!WebSecurity.Initialized) {
                WebSecurity.InitializeDatabaseConnection("SqlAdoCs", "UserTable", "Id", "Name", autoCreateTables: true);
            }
            int idUser = WebSecurity.CurrentUserId;
            int maxParticipant = model.MaxParticipants;
            int nbParticpant = 0;
            if (maxParticipant > 0) {
                nbParticpant = MgtParticipant.GetInstance().GetAllByIdCourse(id).Count;
            }
            String message = "";
            if (maxParticipant > 0 && nbParticpant < maxParticipant) {
                MgtRace.GetInstance().addInscription(id, idUser);
                message = "Vous êtes bien inscrit à la course " + model.Title + " qui commenceras le " + model.DateStart + " " +
                    "à " + model.Town + ".Un email récapitulatif vous a été envoyer.";
            }
            else {
                message = "Il n'y a plus de place pour cette course. Veuillez essayer ultérieument ou participer à une autre course.";
            }
            

            MailUtil mail = new MailUtil();
            // mail.sendMailInscription(id, idUser);

            RegisterView view = new RegisterView();
            view.Course = model;
            view.message = message;
            return View(view);
        }

        //
        // POST: /Race/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RaceModel race)
        {
            try
            {
                var result = MgtRace.GetInstance().UpdateRace(race.ToBo());
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Race/Delete/5
        [Authorize(Roles = "Administrateur")]
        public ActionResult Delete(int id)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // POST: /Race/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var result = MgtRace.GetInstance().RemoveRace(id);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult Inscrits(int id = 0)
        {
            InscritsView view = new InscritsView();

            List<Participant> partipants = MgtParticipant.GetInstance().GetAllByIdCourse(id);
            List<PersonneModel> personnes = new List<PersonneModel>();

            foreach (Participant part in partipants) {
                personnes.Add(MgtPersonne.GetInstance().GetPersonneById(part.IdPersonne).ToModel());
            }

            view.Course = MgtRace.GetInstance().GetRace(id).ToModel();
            view.personnes = personnes;
            view.nbInscrits = personnes.Count;

            return View(view);
        }


        // GET: /Race/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
         {		
             var result = MgtRace.GetInstance().GetRace(id).ToModel();		
             if (result == null)		
             {		
                 return HttpNotFound();		
             }		
 		
             return View(result);		
         }

    }
}
