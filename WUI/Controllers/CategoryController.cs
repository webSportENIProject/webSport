using BLL;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using WUI.Extensions;
using WUI.Models;


namespace WUI.Controllers
{
    public class CategoryController : Controller
    {
        private List<CategoryModel> maListeEnCache;


        //
        // GET: /Category/
        [HttpGet]
        [OutputCache(Duration = 86400,
                     Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            // On récupère les variables permettant le tri de la liste dans la requête HTTP
            // La requête HTTP correspond à l'appel du Controller depuis la View
            var field = Request["field"];
            var dirct = Request["dirct"];

            // Si on fait le tri, alors les éléments ont déjà été chargés en cache
            // Cela évite de faire des appels en bases inutiles et potentiellement longs
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(dirct))
            {
                MgtCategory service = new MgtCategory();
                maListeEnCache = service.GetCategory().ToModels();
            }

            return View(maListeEnCache);
        }
    }
}
