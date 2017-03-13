using BLL;
using System;
using System.Data;
using System.Web.Mvc;
using WUI.Models;
using WUI.Extensions;
using System.Configuration;
using System.Web.UI;


namespace WUI.Controllers
{
    public class DisplayConfigurationController : Controller
    {
        private MgtDisplayConfiguration serviceDc;

        public DisplayConfigurationController()
        {
            serviceDc = new MgtDisplayConfiguration();
        }

        //
        // GET: /DisplayConfiguration/
        [OutputCache(Duration = 7200,
                     VaryByParam = "personneId",
                     Location = OutputCacheLocation.Client)]
        public ActionResult Index(int personneId)
        {
            var result = serviceDc.GetDisplayConfiguration(personneId).ToModels();

            return View(result);
        }

        //
        // GET: /DisplayConfiguration/Details/5

        public ActionResult Details(Guid? id = null)
        {
            DisplayConfigurationModel result = null;

            if (id != null)
            {
                result = serviceDc.GetDisplayConfiguration((Guid)id).ToModel();
                if (result == null)
                {
                    return HttpNotFound();
                }
            }

            return View(result);
        }

        //
        // GET: /DisplayConfiguration/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DisplayConfiguration/Create

        [HttpPost]
        public ActionResult Create(DisplayConfigurationModel dc)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DisplayConfiguration/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            DisplayConfigurationModel result = null;

            if (id != null)
            {
                result = serviceDc.GetDisplayConfiguration((Guid)id).ToModel();
                if (result == null)
                {
                    return HttpNotFound();
                }
            }

            return View(result);
        }

        //
        // POST: /DisplayConfiguration/Edit/5

        [HttpPost]
        public ActionResult Edit(DisplayConfigurationModel dc)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DisplayConfiguration/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            DisplayConfigurationModel result = null;

            if (id != null)
            {
                result = serviceDc.GetDisplayConfiguration((Guid)id).ToModel();
                if (result == null)
                {
                    return HttpNotFound();
                }
            }

            return View(result);
        }

        //
        // POST: /DisplayConfiguration/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}