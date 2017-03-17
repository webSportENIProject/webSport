using BLL;
using BO;
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

        public ActionResult Create()
        {
            PointModel point = new PointModel();
            point.ListTypePointOptions = MgtTypePoint.GetInstance().GetAllItems();
            point.ListCourseOptions = MgtRace.GetInstance().GetAllItems();
            return View(point);
        }

        [HttpPost]
        public ActionResult Create(PointModel point)
        {

            try
            {
                if (ModelState.IsValid && MgtPoint.GetInstance().AddPoint(point.ToBo()))
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
