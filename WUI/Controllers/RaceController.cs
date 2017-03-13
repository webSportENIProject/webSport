using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;
using WUI.Helper;
using WUI.Models;

namespace WUI.Controllers
{
    // Pour appeler n'importe quelle méthode, l'utilisateur doit être connecté
    // Sauf si une méthode lève la condition avec : [AllowAnonymous]
    [Authorize]
    public class RaceController : BaseController
    {
        private MgtRace serviceRace;

        /// <summary>
        /// Constructeur
        /// </summary>
        public RaceController()
        {
            serviceRace = MgtRace.GetInstance();
        }

        //
        // GET: /Race/
        // Tous les utilisateurs peuvent visionnés la liste
        [AllowAnonymous]
        public ActionResult Index(SortType sortType = SortType.DEFAULT)
        {
            //List<BO.Race> result;

            //switch (sortType)
            //{
            //    case SortType.BY_TITLE:
            //        // On passe le delegate en paramètres
            //        var service = new MgtRace(TrierParTitre);
            //        result = service.SortByTitle();
            //        break;

            //    case SortType.BY_TOWN:
            //        // On passe l'expression lambda en paramètres
            //        result = serviceRace.SortByTown((BO.Race r1, BO.Race r2) =>
            //        {
            //            return r1.Town.CompareTo(r2.Town);
            //        });
            //        break;

            //    case SortType.DEFAULT:
            //    default:
            //        result = serviceRace.SortByDateStart();
            //        break;
            //}

            //// #### CHARGEMENT DES DONNEES EN ASYNCHRONE ####
            //// Si on rend l'action Index asynchrone, avec :
            //// => public async Task<ActionResult> Index(SortType sortType = SortType.DEFAULT) { CODE }
            //// On va pouvoir charger les éléments en BDD en asynchrone via la ligne ci-dessous :
            ////result = await serviceRace.GetAllRaceAsync();

            //// Pour pouvoir charger les données en asynchrone et faire une animation visuelle,
            //// il faudra créer une nouvelle action 'LoadRace()' qui fasse uniquement le chargement asynchrone
            //// des données et l'appeler avec Ajax (jQuery) :
            //// $.ajax(
            ////      url: '/Race/LoadRace'
            ////      beforeSend: function () { // lancer l'animation visuel }
            //// ).success(function (data) {
            ////      // Mettre les données dans la liste grâce à 'data'
            //// }).error(function (data) {
            ////      // Afficher une erreur (pop-up) ou laisser la liste vide : aucun résultat trouvé
            //// }).complete(function (data) {
            ////      // Arrêter l'animation
            //// });
            //// #### CHARGEMENT DES DONNEES EN ASYNCHRONE ####

            //return View(result.ToModels(true));
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            RouteData.Values["culture"] = culture; // set culture

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture; // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }



        public int TrierParTitre(BO.Race r1, BO.Race r2)
        {
            return r1.Title.CompareTo(r2.Title);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Autocomplete(string term)
        {
            var result = serviceRace.GetRace(term).ToModels();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Race/Details/5
        public ActionResult Details(int id)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // GET: /Race/Create
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
                // Met en évidence le ValidationSummary(false) dans la vue
                //ModelState.AddModelError(string.Empty, "Erreur visble via le ValidationSummary(true) mais pas par le ValidationSummary(false)");

                if (ModelState.IsValid
                    && serviceRace.AddRace(race.ToBo()))
                {
                    //MgtRace.GetInstance()
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
        public ActionResult Edit(int id = 0)
        {
            var result = MgtRace.GetInstance().GetRace(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // POST: /Race/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RaceModel race)
        {
            try
            {
                var result = serviceRace.UpdateRace(race.ToBo());
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
                var result = serviceRace.RemoveRace(id);
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
    }
}
