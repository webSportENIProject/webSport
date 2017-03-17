using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WUI.Controllers
{
    public class ResultatController : Controller
    {
        //
        // GET: /Resultat/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string ext = Path.GetExtension(_FileName);
                    if (ext.Equals(".csv"))
                    {
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                        MgtResultat.GetInstance().DoTraitementImport(_path);
                        ViewBag.Message = "Upload du fichier réussi";
                    }
                    else {
                        ViewBag.Message = "Veuillez uploader seulement un fichier csv";
                    }
                }
                else {
                    ViewBag.Message = "Erreur fichier vide";
                }

                return View();
            }
            catch
            {
                ViewBag.Message = "Upload du fichier réussi en echec";
                return View();
            }
        }

    }
}
