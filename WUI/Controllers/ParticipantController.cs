using BLL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebMatrix.WebData;
using WUI.Extensions;
using WUI.Models;

namespace WUI.Controllers
{
    public class ParticipantController : Controller
    {
        //
        // GET: /Participant/
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("SqlAdoCs", "UserTable", "Id", "Name", autoCreateTables: true);
            }
            //Récupération de la personne par son id UserTable
            int idUser = WebSecurity.CurrentUserId;
            var personne = MgtPersonne.GetInstance().GetPersonneByIdUserTable(idUser).ToModel();
            int idPersonne = personne.Id;

            //Récupération de la liste des inscriptions de la personne par son id
            var inscriptions = MgtParticipant.GetInstance().GetAllById(idPersonne).ToModels();

            //Réupérations des informations de chaque course auquel la personne est inscrite
            //On génère une liste des courses auxquelles la personne est inscrite, avec tous les détails les concernant
            List<RaceModel> coursesInscrit = new List<RaceModel>();
            foreach (var ins in inscriptions)
            {
                var course = MgtRace.GetInstance().GetRace(ins.CourseId).ToModel();
                coursesInscrit.Add(course);
            }

            //On retourne la vue corespondante avec la liste des inscriptions en paramètre
            return View(coursesInscrit);
        }
    }
}
