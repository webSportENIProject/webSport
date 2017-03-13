using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ErreurGenerale()
        {
            return View("Error");
        }

        public ActionResult Erreur404()
        {
            return View("Page404");
        }

        //public ActionResult ErreurHttpNotFound()
        //{
        //    return View("NotFound");
        //}
    }
}
