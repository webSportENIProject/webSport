using BLL;
using System.Web.Mvc;
using WUI.Extensions;


namespace WUI.Controllers
{
    public class ParticipantController : Controller
    {
        //
        // GET: /Competitor/
        [HttpGet]
        public ActionResult Index()
        {
            var result = MgtRace.GetInstance().GetAllItems().ToModels();
            return View(result);
        }
    }
}
