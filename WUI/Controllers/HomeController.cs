﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WUI.Extensions;
using WUI.Models;

namespace WUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modifiez ce modèle pour dynamiser votre application ASP.NET MVC.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Votre page de description d’application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Votre page de contact.";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(MailModel mail)
        {
            if (ModelState.IsValid)
            {
                MgtMail.GetInstance().AddMail(mail.ToBo());
            }
            return RedirectToAction("Index");
        }
    }
}
