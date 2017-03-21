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
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            List<ContactModel> result = MgtMail.GetInstance().GetAllContact().ToModels();
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Votre page de contact.";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactModel mail)
        {
            if (ModelState.IsValid)
            {
                MgtMail.GetInstance().AddMail(mail.ToBo());
            }
            return RedirectToAction("Index");
        }

    }
}
