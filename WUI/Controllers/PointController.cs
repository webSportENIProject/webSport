﻿using BLL;
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
        public ActionResult Index(int id = 0)
        {
            PointView view = new PointView();
            List<PointModel> points = MgtPoint.GetInstance().GetAllWithCourseAndTypePointByCourse(id).ToModels();
            RaceModel race = MgtRace.GetInstance().GetRace(id).ToModel();

            view.points = points.OrderBy(x => x.Ordre).ToList();
            view.idCourse = id;
            view.libelleCourse = race.Title;

            return View(view);
        }

        public ActionResult Create(int id = 0)
        {
            PointModel point = new PointModel();
            point.ListTypePointOptions = MgtTypePoint.GetInstance().GetAllItems();
            point.IdCourse = id;
            point.Ordre = MgtPoint.GetInstance().GetAllWithCourseAndTypePointByCourse(id).Count;
            return View(point);
        }

        [HttpPost]
        public ActionResult Create(PointModel point)
        {

            try
            {
                if (ModelState.IsValid && MgtPoint.GetInstance().AddPoint(point.ToBo()))
                {
                    return RedirectToAction("Index", new { id = point.IdCourse });
                }
                else
                {
                    point.ListTypePointOptions = MgtTypePoint.GetInstance().GetAllItems();
                    return View(point);
                }

            }
            catch
            {
                point.ListTypePointOptions = MgtTypePoint.GetInstance().GetAllItems();
                return View(point);
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
                    return RedirectToAction("Index", new { id = point.IdCourse });
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
       [Authorize(Roles = "Administrateur")]
        public ActionResult Delete(int idPoint)
        {
            var result = MgtPoint.GetInstance().GetPointById(idPoint).ToModel();
            RaceModel race = MgtRace.GetInstance().GetRace(result.IdCourse).ToModel();
            TypePointModel typePoint = MgtTypePoint.GetInstance().GetTypePoint(result.IdTypePoint).ToModel();

            result.LibelleCourse = race.Title;
            result.LibelleTypePoint = typePoint.Libelle;

            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        //
        // POST: /Race/Delet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int idPoint, FormCollection collection)
        {
            try
            {
                PointModel point = MgtPoint.GetInstance().GetPointById(idPoint).ToModel();
                List<Resultat> listResultats = MgtResultat.GetInstance().GetAllByPoint(idPoint).ToList();
                //Suppression des résultats associés au point
                foreach (Resultat resultat in listResultats)
                {
                    MgtResultat.GetInstance().DeleteResultat(resultat.id);
                }

                int ordre = point.Ordre;
                int taille = MgtPoint.GetInstance().GetAllWithCourseAndTypePointByCourse(point.IdCourse).Count;
                if (ordre < taille - 1) {
                    while (ordre < taille - 1) {
                        ordre++;
                        Point pointSup = MgtPoint.GetInstance().GetPointByCourseAndOrder(point.IdCourse, ordre);
                        pointSup.Ordre = ordre - 1;
                        MgtPoint.GetInstance().UpdatePoint(pointSup);
                    }
                }

                var result = MgtPoint.GetInstance().DeletePoint(idPoint);
                if (result)
                {
                    return RedirectToAction("Index", new { id = point.IdCourse });
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
        public ActionResult Up(int id = 0)
        {
            Point point = MgtPoint.GetInstance().GetPointById(id);
            int oldOrdre = point.Ordre;
            int newOrdre = point.Ordre - 1;
            Point pointToEchange = MgtPoint.GetInstance().GetPointByCourseAndOrder(point.CourseId, newOrdre);
            point.Ordre = newOrdre;
            pointToEchange.Ordre = oldOrdre;
            MgtPoint.GetInstance().UpdatePoint(point);
            MgtPoint.GetInstance().UpdatePoint(pointToEchange);

            return RedirectToAction("Index", new { id = point.CourseId });
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult Down(int id = 0)
        {
            Point point = MgtPoint.GetInstance().GetPointById(id);
            int oldOrdre = point.Ordre;
            int newOrdre = point.Ordre + 1;
            Point pointToEchange = MgtPoint.GetInstance().GetPointByCourseAndOrder(point.CourseId, newOrdre);
            point.Ordre = newOrdre;
            pointToEchange.Ordre = oldOrdre;
            MgtPoint.GetInstance().UpdatePoint(point);
            MgtPoint.GetInstance().UpdatePoint(pointToEchange);

            return RedirectToAction("Index", new { id = point.CourseId });
        }

    }
}
