using BLL;
using System.Web.Mvc;
using WUI.Extensions;


namespace WUI.Controllers
{
    public class CompetitorController : Controller
    {
        //
        // GET: /Competitor/
        [HttpGet]
        public ActionResult Index()
        {
            MgtCompetitor service = new MgtCompetitor();
            var result = service.GetCompetitor().ToModels();

            return View(result);
        }
    }
}
