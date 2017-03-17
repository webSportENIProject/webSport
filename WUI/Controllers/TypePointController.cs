using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using WUI.Extensions;
using WUI.Models;

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

        // GET: /TypePoint/
        [Authorize(Roles = "Administrateur")]
        public ActionResult Index()
        {
            List<TypePointModel> result = MgtTypePoint.GetInstance().GetAllItems().ToModels();
            return View(result);
        }


    }
}
