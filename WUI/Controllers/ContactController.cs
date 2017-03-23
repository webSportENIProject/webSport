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
        [Authorize(Roles = "Administrateur")]
        public ActionResult Index(int page = 1)
        {
            ContactView view = new ContactView();
            List<ContactModel> result = MgtMail.GetInstance().GetAllContact().ToModels(true).ToList();
            Pager pager = new Pager(result.Count(), page,16);
            view.contact = result.OrderByDescending(x => x.Date).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            view.Pager = pager;

            return View(view);
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
                return RedirectToAction("Index", "Home");
            }else
            {
                return View();
            }
           
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult Details(int id = 0)
        {
            ContactModel model = MgtMail.GetInstance().Get(id).ToModel();
            return View(model);
        }

    }
}
