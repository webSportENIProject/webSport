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

        //
        // GET: /TypePoint/Create
        [Authorize(Roles = "Administrateur")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TypePoint/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypePointModel typePoint)
        {
            try
            {
                if (ModelState.IsValid && MgtTypePoint.GetInstance().AddTypePoint(typePoint.ToBo()))
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
        // GET: /TypePoint/Edit/{id}
        [Authorize(Roles = "Administrateur")]
        public ActionResult Edit(int id = 0)
        {
            var result = MgtTypePoint.GetInstance().GetTypePoint(id).ToModel();
            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }


        //
        // POST: /TypePoint/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TypePointModel typePoint)
        {
            try
            {
                var result = MgtTypePoint.GetInstance().UpdateTypePoint(typePoint.ToBo());
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
        }

    }
}
