using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;
using WUI.Models;

namespace WUI.Controllers
{
    public class PointController : Controller
    {
        //
        // GET: /Point/

        public ActionResult Index()
        {
            List<PointModel> points = MgtPoint.GetInstance().GetALLWithCourseAndTypePoint().ToModels();
            return View(points);
        }

    }
}
