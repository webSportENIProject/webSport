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
        [Authorize(Roles = "Administrateur")]
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

        [Authorize(Roles = "Administrateur")]
        public ActionResult Update(int idPoint = 0)
        {
            PointModel point = MgtPoint.GetInstance().GetPointById(idPoint).ToModel();
            point.ListTypePointOptions = MgtTypePoint.GetInstance().GetAllItems();
            point.ListCourseOptions = MgtRace.GetInstance().GetAllItems();
            return View(point);
        }

        [HttpPost]
        public ActionResult Update(PointModel point)
        {
            try
            {
                if (ModelState.IsValid && MgtPoint.GetInstance().UpdatePoint(point.ToBo()))
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
        // GET: /Race/Delete
       /* [Authorize(Roles = "Administrateur")]
        public ActionResult Delete(int id)
        {
            var result = MgtPoint.GetInstance().GetPointById(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // POST: /Race/Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var result = MgtPoint.GetInstance().RemovePoint(id);
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
        }*/

    }
}
