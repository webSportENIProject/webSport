using BLL;
using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using WUI.Extensions;
using WUI.Models;

namespace WUI.Controllers
{
    [Authorize]
    public class ResultatController : Controller
    {
        //
        // GET: /Resultat/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrateur")]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur")]
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

        public ActionResult Resultat(int id = 0)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("SqlAdoCs", "UserTable", "Id", "Name", autoCreateTables: true);
            }
            int idUser = WebSecurity.CurrentUserId;

            List<Resultat> resultats = MgtResultat.GetInstance().GetAllByCourseAndPersonne(id, idUser);
            Race course = MgtRace.GetInstance().GetRace(id);
            List<ResultatModel> models = new List<ResultatModel>();
            Boolean first = true;
            DateTime debut = DateTime.Now;
            foreach (Resultat bo in resultats) {
                ResultatModel model = bo.ToModel();
                if (first)
                {
                    debut = model.temps;
                    model.tempsPoint = "0:00:00.000";
                    first = false;
                }
                else
                {
                    TimeSpan span = model.temps.Subtract(debut);
                    model.tempsPoint = formatTemps(span);
                }
                model.Point = MgtPoint.GetInstance().GetPointById(model.idPoint).ToModel();
                models.Add(model);
            }
            models.OrderBy(x => x.Point.Ordre);
            ResultatView view = new ResultatView() {
                Course = course.ToModel(),
                resultats = models
            };

            return View(view);
        }

        private String formatTemps(TimeSpan span)
        {
            String temps = span.Hours + ":";

            if (span.Minutes < 10) {
                temps += "0" + span.Minutes + ":";
            } else {
                temps += span.Minutes + ":";
            }

            if (span.Seconds < 10) {
                temps += "0" + span.Seconds + ".";
            }
            else {
                temps += span.Seconds + ".";
            }

            if (span.Milliseconds < 10) {
                temps += "00" + span.Milliseconds;
            }
            else if (span.Milliseconds < 100) {
                temps += "0" + span.Milliseconds;
            }
            else {
                temps += span.Milliseconds;
            }

            return temps;
        }
    }
}
