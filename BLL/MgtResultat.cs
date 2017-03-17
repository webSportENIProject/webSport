﻿using BO;
using DAL;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace BLL
{
    public class MgtResultat
    {

        private static MgtResultat _instance;

        public static MgtResultat GetInstance()
        {
            if (_instance == null)
                _instance = new MgtResultat();
            return _instance;
        }

        private UnitOfWork _uow { get; set; }

        private MgtResultat()
        {
            // Récupération des données via la DAL (informations stockées dans une base de données SQL)
            _uow = new UnitOfWork();
        }

        public void DoTraitementImport(string filepath)
        {
            string[] allLines = File.ReadAllLines(filepath);
            List<Resultat> resultats = new List<Resultat>();
            foreach (String lines in allLines)
            {
                string[] data = lines.Split(';');
                int idPersonne = int.Parse(data[0]);
                int idCourse = int.Parse(data[1]);
                int idPoint = int.Parse(data[2]);
                string temps = data[3];
                DateTime dateTimeTemps = DateTime.ParseExact(temps,
                                                      "dd/MM/yyyy HH:mm:ss.fff",
                                                     System.Globalization.CultureInfo.InvariantCulture);
                resultats.Add(new Resultat()
                {
                    idCourse = idCourse,
                    idPersonne = idPersonne,
                    idPoint = idPoint,
                    temps = dateTimeTemps
                });
            }
            this._uow.ResultatRepo.AddAll(resultats);
        }

        public bool AddResultat(Resultat resultat)
        {
            if (resultat != null)
            {

                int lastId = this._uow.ResultatRepo.Add(resultat);
                if (lastId > 0)
                {
                    resultat.id = lastId;
                }
                return true;
            }

            return false;
        }

        public Resultat GetResultat(int id)
        {
            return this._uow.ResultatRepo.GetById(id);
        }

        public List<Resultat> GetAllByCourseAndPersonne(int idCourse, int idPersonne)
        {
            return this._uow.ResultatRepo.GetAllByCourseAndPersonne(idCourse, idPersonne);
        }

    }
}