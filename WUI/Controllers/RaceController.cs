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
//For converting HTML TO PDF- START
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.util;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
//For converting HTML TO PDF- END


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
            result = result.Where(x => x.DateEnd >= DateTime.Today ).ToList();

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
                    result.ElementAt(i).Inscrit = inscrit;
                }
            }
            


            return View(result.OrderByDescending(x => x.DateStart).ToList());
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult Old(int page = 1)
        {
            OldRaceView view = new OldRaceView();
            List<RaceModel> result = MgtRace.GetInstance().GetAllItemsWithParticipants().ToModels(true);
            result = result.Where(x => x.DateEnd < DateTime.Today).ToList();
            Pager pager = new Pager(result.Count(), page);
            view.races = result.OrderByDescending(x => x.DateStart).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            view.Pager = pager;

            return View(view);
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
                JavaScriptSerializer js = new JavaScriptSerializer();
                LatLng[] listePoints = js.Deserialize<LatLng[]>(race.AjaxPoints);
                bool valid = true;

                if (ModelState.IsValid)
                {
                    //Ajout de la course
                    race = MgtRace.GetInstance().AddRace(race.ToBo()).ToModel();

                    //Ajout des points
                    for (int i = 0; i < listePoints.Length; i++)
                    {
                        Point point = new Point();
                        point.Titre = "Titre " + i;
                        point.Latitude = listePoints[i].lat;
                        point.Longitude = listePoints[i].lng;
                        point.Ordre = i;
                        // VALEUR HARCODE => //TODO: modify
                        if (i == 0) {
                            point.TypePointId = 1; 
                        } else if (i == listePoints.Length - 1) {
                            point.TypePointId = 2; 
                        }
                        else {
                            point.TypePointId = 4; //Correspond au KM
                        }
                        point.CourseId = race.Id;
                        valid = valid && MgtPoint.GetInstance().AddPoint(point);
                    }

                    if (valid) {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
            if (maxParticipant == 0 || nbParticpant < maxParticipant) {
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
        public ActionResult Inscrits(int id = 0, int page = 1)
        {
            InscritsView view = new InscritsView();
            
            List<Participant> partipants = MgtParticipant.GetInstance().GetAllByIdCourse(id);
            List<PersonneModel> personnes = new List<PersonneModel>();

            foreach (Participant part in partipants) {
                PersonneModel model = MgtPersonne.GetInstance().GetPersonneById(part.IdPersonne).ToModel();
                model.participant = part.ToModel();     
                personnes.Add(model);
            }

            Pager pager = new Pager(personnes.Count, page, 15);

            view.Course = MgtRace.GetInstance().GetRace(id).ToModel();
            view.personnes = personnes.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            view.nbInscrits = personnes.Count;
            view.inscriptions = initInscriptions(partipants);
            view.Pager = pager;

            return View(view);
        }

        [AllowAnonymous]
        public ActionResult InscritsToPDF(int id)
        {
            InscritsView view = new InscritsView();

            List<Participant> partipants = MgtParticipant.GetInstance().GetAllByIdCourse(id);
            List<PersonneModel> personnes = new List<PersonneModel>();

            foreach (Participant part in partipants)
            {
                PersonneModel model = MgtPersonne.GetInstance().GetPersonneById(part.IdPersonne).ToModel();
                model.participant = part.ToModel();
                personnes.Add(model);
            }

            view.Course = MgtRace.GetInstance().GetRace(id).ToModel();
            view.personnes = personnes;
            view.nbInscrits = personnes.Count;
            view.inscriptions = initInscriptions(partipants);

            Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);
            string html = RenderRazorViewToString("~/Views/Race/InscritsToPDF.cshtml", view);
            TextReader reader = new StringReader(html);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, output);

                // step 3: we create a worker parse the document
                HTMLWorker worker = new HTMLWorker(doc);

                doc.Open();

                worker.StartDocument();

                // step 5: parse the html into the document
                worker.Parse(reader);

                // step 6: close the document and the worker
                worker.EndDocument();
                worker.Close();

                doc.Close();
                return File(output.ToArray(), "application/pdf", "Inscriptions" + view.Course.Title + ".pdf");
            }
        }

        private Dictionary<DateTime, int> initInscriptions(List<Participant> partipants) {
            partipants = partipants.OrderBy(x => x.dateInscription).ToList();
            Dictionary<DateTime, int> inscriptions = new Dictionary<DateTime, int>();
            int nbInscriptions = 0;
            DateTime date = DateTime.MinValue;
            foreach (Participant participant in partipants) {
                if (date == DateTime.MinValue) {
                    date = participant.dateInscription;
                } else if (date != participant.dateInscription) {
                    inscriptions.Add(date, nbInscriptions);
                    nbInscriptions = 0;
                    date = participant.dateInscription;
                }

                nbInscriptions++;
            }
            inscriptions.Add(date, nbInscriptions);

            return inscriptions;
        }

        // GET: /Race/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
         {		
             RaceModel result = MgtRace.GetInstance().GetRace(id).ToModel();

            int idUser = 0;

            //On récupère l'ID de l'utilisateur courant
            if (WebSecurity.IsAuthenticated)
            {
                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("SqlAdoCs", "UserTable", "Id", "Name", autoCreateTables: true);
                }
                idUser = WebSecurity.CurrentUserId;
            }

            //On récupère l'utisateur
            //On personnalise la vue en f(x) du paramètrage du compte
            if (idUser > 0)
            {
                Personne user = MgtPersonne.GetInstance().GetPersonneByIdUserTable(idUser);
                if (user.kms)
                {
                    result.KmOrMiles = "Km";
                }else if (user.miles)
                {
                    result.KmOrMiles = "Miles";
                } else
                {
                    result.KmOrMiles = "Km";
                }
            }

            if (result == null)		
             {		
                 return HttpNotFound();		
             }		
 		
             return View(result);		
         }

        // GET: /Race/Points/5
        [AllowAnonymous]
        public JsonResult Points(int id)
        {
            RaceModel result = MgtRace.GetInstance().GetRaceWithPoints(id).ToModel(false, true);
            result.Points = result.Points.OrderBy(x => x.Ordre).ToList();
            if (result == null)
            {
                return Json("");
            }
            return Json(result.Points, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult CreatePDF(int id)
        {
            RaceModel result = MgtRace.GetInstance().GetRaceWithPoints(id).ToModel(false, true);
            Document doc = new Document(PageSize.LETTER, 50, 50, 50, 50);
            string html = RenderRazorViewToString("~/Views/Race/CreatePDF.cshtml", result);
            TextReader reader = new StringReader(html);
            
            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, output);

                // step 3: we create a worker parse the document
                HTMLWorker worker = new HTMLWorker(doc);

                doc.Open();

                worker.StartDocument();

                // step 5: parse the html into the document
                worker.Parse(reader);

                // step 6: close the document and the worker
                worker.EndDocument();
                worker.Close();

                doc.Close();
                return File(output.ToArray(), "application/pdf", result.Title+".pdf");
            }

        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult InscriptionAge(int id = 0)
        {
            InscritsView view = new InscritsView();

            List<Participant> partipants = MgtParticipant.GetInstance().GetAllByIdCourse(id);
            List<PersonneModel> personnes = new List<PersonneModel>();

            int Nb20 = 0;
            int Nb21_30 = 0;
            int Nb31_40 = 0;
            int Nb41_50 = 0;
            int Nb51_60 = 0;
            int Nb61 = 0;

            foreach (Participant part in partipants)
            {
                PersonneModel model = MgtPersonne.GetInstance().GetPersonneById(part.IdPersonne).ToModel();
                if (model.DateNaissance != null)
                {
                    int age = DateTime.Now.Year - model.DateNaissance.Value.Year;
                    if (age <= 20)
                    {
                        Nb20++;
                    }
                    else if (age <= 30)
                    {
                        Nb21_30++;
                    }
                    else if (age <= 40)
                    {
                        Nb31_40++;
                    }
                    else if (age <= 50)
                    {
                        Nb41_50++;
                    }
                    else if (age <= 60)
                    {
                        Nb51_60++;
                    }
                    else {
                        Nb61++;
                    }
                }

                personnes.Add(model);
            }

            view.Course = MgtRace.GetInstance().GetRace(id).ToModel();
            view.personnes = personnes;
            view.nbInscrits = personnes.Count;

            view.TA20 = Nb20;
            view.TA21_30 = Nb21_30;
            view.TA31_40 = Nb31_40;
            view.TA41_50 = Nb41_50;
            view.TA51_60 = Nb51_60;
            view.TA61 = Nb61;

            return View(view);
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult InscriptionJour(int id = 0)
        {
            InscritsView view = new InscritsView();

            List<Participant> partipants = MgtParticipant.GetInstance().GetAllByIdCourse(id);
            view.Course = MgtRace.GetInstance().GetRace(id).ToModel();
            view.inscriptions = initInscriptions(partipants);

            return View(view);
        }


    }
}
