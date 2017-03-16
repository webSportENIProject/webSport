using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Controllers
{
    public class TypePointController : Controller
    {

        /// <summary>
        /// Constructeur
        /// </summary>
        public TypePointController()
        {
        }

        //
        // GET: /TypePoint/

        public ActionResult Index()
        {
            return View();
        }

    }
}
