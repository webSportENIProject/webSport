using BO;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Schema;

namespace DAL
{
    public class XmlRace
    {
        #region Singleton

        private static XmlRace _instance;
        public static XmlRace GetInstance()
        {
            if (_instance == null)
                _instance = new XmlRace();
            return _instance;
        }

        #endregion

        #region Constantes

        private const string ATTR_XMLNS = "http://www.eni-ecole.fr/websport";

        private const string NODE_RACE = "race";
        private const string NODE_COMPETITOR = "competitor";
        private const string NODE_ORGANISER = "organiser";

        private const string NODE_ID = "id";

        private const string NODE_TITLE = "title";
        private const string NODE_DESCRIPTION = "description";
        private const string NODE_DATESTART = "datestart";
        private const string NODE_DATEEND = "dateend";
        private const string NODE_TOWN = "ville";

        private const string NODE_NOM = "nom";
        private const string NODE_PRENOM = "prenom";
        private const string NODE_EMAIL = "email";
        private const string NODE_PHONE = "phone";
        private const string NODE_DATENAISSANCE = "datenaissance";

        #endregion

        #region Propriétés publiques & privées
        
        public string CheminXml
        {
            get
            {
                return "~/App_Data/Xml/Race.xml";
            }
        }
        public string CheminXsd
        {
            get
            {
                return "~/App_Data/Xml/Race.xsd";
            }
        }

        private bool _isFileAvailable;
        public bool IsFileAvailable
        {
            get
            {
                return _isFileAvailable;
            }
        }

        public XmlDocument documentXml { get; set; }
        
        #endregion

        #region Constructeur

        public XmlRace()
        {
            _isFileAvailable = OpenOrCreateXml();
            if (!_isFileAvailable)
                throw new Exception(string.Format("Impossible de créer le fichier '{0}'", this.CheminXml));
        }

        #endregion

        #region Public methods

        public bool OpenOrCreateXml()
        {
            bool retour = true;

            try
            {
                // Création de l'objet XmlDocument contenant nos données afin de pouvoir les exploiter
                documentXml = new XmlDocument();

                // Prologue du fichier, ce qui correspond à la création de cette ligne : <?xml version="1.0" encoding="utf-8" ?>
                XmlDeclaration xmlPrologue = default(XmlDeclaration);
                xmlPrologue = documentXml.CreateXmlDeclaration("1.0", "utf-8", "no");

                // On ajoute le prologue dans le document XML
                documentXml.AppendChild(xmlPrologue);

                // Création de l'élément racine (balise <races>) qui sera vide
                XmlElement elementRoot = documentXml.CreateElement("races");

                // On met l'élément racine dans le document XML
                documentXml.AppendChild(elementRoot);

                // On définit l'espace de nom de notre fichier
                // Cela se représente comme un attribut à la balise racine <races>
                XmlAttribute xmlns = documentXml.CreateAttribute("xmlns");
                xmlns.Value = "http://www.eni-ecole.fr/websport";
                elementRoot.Attributes.Append(xmlns);

                foreach (var race in GetInitializeList())
                {
                    AddRaceXml(race);
                }

                // On enregistre le fichier
                documentXml.Save(HttpContext.Current.Server.MapPath(this.CheminXml));


                // On peut vérifier la conformité du fichier pour s'assurer qu'il soit conforme au
                // fichier de validation Race.xsd
                documentXml.Load(HttpContext.Current.Server.MapPath(this.CheminXml));
                documentXml.Schemas.Add(ATTR_XMLNS, HttpContext.Current.Server.MapPath(this.CheminXsd));
                var xmlIsInvalid = false;
                documentXml.Validate((sender, args) =>
                {
                    xmlIsInvalid = true;
                });
                if (xmlIsInvalid)
                {
                    // Si le fichier n'est pas conforme au XSD, cet événement est déclenché
                    throw new Exception(string.Format("Le fichier XML '{0}' possède une (ou des) erreur(s)...", HttpContext.Current.Server.MapPath(this.CheminXsd)));
                }



                // Si on veut lire le fichier XML de manière simple, en construisant un objet de type XmlDocument
                // il suffit de décommenter le code ci-dessous :
                //FileStream fs = new FileStream("cheminversmon.xml", FileMode.OpenOrCreate, FileAccess.Read);
                //documentXml.Load(fs);
            }
            catch
            {
                retour = false;
            }

            return retour;
        }

        public bool AddRaceXml(Race race)
        {
            if (race == null) return false;

            bool retour = true;

            try
            {
                // On récupère l'élément racine
                XmlElement elementRoot = documentXml.DocumentElement;

                // Création de l'élément race (balise <race>)
                XmlElement elementRace = documentXml.CreateElement(NODE_RACE);

                // Création des propriétés de la race : Titre, Description, DateStart, DateEnd, Town
                // On met chaque élément dans l'élément <race>
                XmlElement elementIdRace = documentXml.CreateElement(NODE_ID);
                elementIdRace.InnerText = race.Id.ToString();
                elementRace.AppendChild(elementIdRace);
                XmlElement elementTitre = documentXml.CreateElement(NODE_TITLE);
                elementTitre.InnerText = race.Title;
                elementRace.AppendChild(elementTitre);
                XmlElement elementDescription = documentXml.CreateElement(NODE_DESCRIPTION);
                elementDescription.InnerText = race.Description;
                elementRace.AppendChild(elementDescription);
                XmlElement elementDateStart = documentXml.CreateElement(NODE_DATESTART);
                elementDateStart.InnerText = race.DateStart.ToString();
                elementRace.AppendChild(elementDateStart);
                XmlElement elementDateEnd = documentXml.CreateElement(NODE_DATEEND);
                elementDateEnd.InnerText = race.DateEnd.ToString();
                elementRace.AppendChild(elementDateEnd);
                XmlElement elementTown = documentXml.CreateElement(NODE_TOWN);
                elementTown.InnerText = race.Town;
                elementRace.AppendChild(elementTown);

                if (race.Competitors != null)
                {
                    // Création de l'élément competitor (qui est une personne)
                    List<Personne> listPerson = race.Competitors.Select(x => (Personne)x).ToList();
                    XmlElement elementRootCompet = PreparePersonXml(listPerson, NODE_COMPETITOR);

                    // On met l'élément <competitors> dans l'élément <race>
                    elementRace.AppendChild(elementRootCompet);
                }

                // Création des Organisers
                if (race.Organisers != null)
                {
                    // Création de l'élément Organisateur (qui est une personne)
                    var listPerson = race.Organisers.Select(x => (Personne)x).ToList();
                    XmlElement elementRootOrgan = PreparePersonXml(listPerson, NODE_ORGANISER);

                    // On met l'élément <organisers> dans l'élément <race>
                    elementRace.AppendChild(elementRootOrgan);
                }

                // On ajoute l'élément <race> dans l'élément racine
                elementRoot.AppendChild(elementRace);

                // On enregistre le fichier
                documentXml.Save(HttpContext.Current.Server.MapPath(this.CheminXml));
            }
            catch (Exception)
            {
                retour = false;
            }
            
            return retour;
        }

        public List<Race> GetRace()
        {
            List<Race> list = new List<Race>();

            // Récupération de l'élément racine <races>
            XmlElement elementRoot = documentXml.DocumentElement;

            // Récupération de chaque élément <race>
            foreach (XmlElement elementRace in elementRoot.ChildNodes)
            {
                Race race = new Race()
                {
                    Id = int.Parse(elementRace[NODE_ID].InnerText),
                    Title = elementRace[NODE_TITLE].InnerText,
                    Description = elementRace[NODE_DESCRIPTION].InnerText,
                    DateStart = DateTime.Parse(elementRace[NODE_DATESTART].InnerText ?? string.Empty),
                    DateEnd = DateTime.Parse(elementRace[NODE_DATEEND].InnerText ?? string.Empty),
                    Town = elementRace[NODE_TOWN].InnerText,
                    Competitors = new List<Competitor>(),
                    Organisers = new List<Organizer>()
                };

                // Récupération de chaque élément <competitor> si la balise <competitors> existe
                var elementRootCompet = elementRace[NODE_COMPETITOR + "s"] != null ? elementRace[NODE_COMPETITOR + "s"].ChildNodes : null;
                if (elementRootCompet != null)
                {
                    foreach (XmlElement elementCompet in elementRootCompet)
                    {
                        Competitor comp = new Competitor()
                        {
                            Id = int.Parse(elementCompet[NODE_ID].InnerText),
                            Nom = elementCompet[NODE_NOM].InnerText,
                            Prenom = elementCompet[NODE_PRENOM].InnerText,
                            Email = elementCompet[NODE_EMAIL].InnerText,
                            Phone = elementCompet[NODE_PHONE].InnerText,
                            DateNaissance = DateTime.Parse(elementCompet[NODE_DATENAISSANCE].InnerText)
                        };
                        race.Competitors.Add(comp);
                    }
                }

                // Récupération de chaque élément <organiser> si la balise <organisers> existe
                var elementRootOrgan = elementRace[NODE_ORGANISER + "s"] != null ? elementRace[NODE_ORGANISER + "s"].ChildNodes : null;
                if (elementRootOrgan != null)
                {
                    foreach (XmlElement elementOrgan in elementRootOrgan)
                    {
                        Organizer org = new Organizer()
                        {
                            Id = int.Parse(elementOrgan[NODE_ID].InnerText),
                            Nom = elementOrgan[NODE_NOM].InnerText,
                            Prenom = elementOrgan[NODE_PRENOM].InnerText,
                            Email = elementOrgan[NODE_EMAIL].InnerText,
                            Phone = elementOrgan[NODE_PHONE].InnerText,
                            DateNaissance = DateTime.Parse(elementOrgan[NODE_DATENAISSANCE].InnerText)
                        };
                        race.Organisers.Add(org);
                    }
                }

                list.Add(race);
            }
            
            return list;
        }

        #endregion

        #region Private methods
        
        private XmlElement PreparePersonXml(List<Personne> persons, string elementName)
        {
            // Création de l'élément organisers (balise <race>)
            XmlElement elementRootPerson = documentXml.CreateElement(string.Format("{0}s", elementName));

            // Création de chaque organisateur, un par un
            foreach (var pers in persons)
            {
                // Création de l'élément organiser (balise <race>)
                XmlElement elementPers = documentXml.CreateElement(elementName);

                // Création des propriétés de l'organisateur : Id, Nom, Prenom, Email, Phone, DateNaissance
                // On met chaque élément dans l'élément <organiser>
                XmlElement elementIdCompet = documentXml.CreateElement(NODE_ID);
                elementIdCompet.InnerText = pers.Id.ToString();
                elementPers.AppendChild(elementIdCompet);
                XmlElement elementNomCompet = documentXml.CreateElement(NODE_NOM);
                elementNomCompet.InnerText = pers.Nom;
                elementPers.AppendChild(elementNomCompet);
                XmlElement elementPrenomCompet = documentXml.CreateElement(NODE_PRENOM);
                elementPrenomCompet.InnerText = pers.Prenom;
                elementPers.AppendChild(elementPrenomCompet);
                XmlElement elementEmailCompet = documentXml.CreateElement(NODE_EMAIL);
                elementEmailCompet.InnerText = pers.Email;
                elementPers.AppendChild(elementEmailCompet);
                XmlElement elementPhoneCompet = documentXml.CreateElement(NODE_PHONE);
                elementPhoneCompet.InnerText = pers.Phone;
                elementPers.AppendChild(elementPhoneCompet);
                if (pers.DateNaissance != null)
                {
                    XmlElement elementDateNaissCompet = documentXml.CreateElement(NODE_DATENAISSANCE);
                    elementDateNaissCompet.InnerText = pers.DateNaissance.ToString();
                    elementPers.AppendChild(elementDateNaissCompet);
                }

                // On met l'élément <organiser> dans l'élément <organisers>
                elementRootPerson.AppendChild(elementPers);
            }

            return elementRootPerson;
        }

        private List<Race> GetInitializeList()
        {
            var listRace = new List<Race>
            {
                new Race
                {
                    Id = 1,
                    Title = "Foulées de l'éléphant",
                    Description = "Course nocturne de 10km sur l'Île de Nantes",
                    DateStart = new DateTime(2017, 4, 27, 21, 0, 0),
                    DateEnd = new DateTime(2017, 4, 28, 0, 0, 0),
                    Town = "Nantes",

                    Competitors = new List<Competitor>
                    {
                        new Competitor
                        {
                            Id = 1,
                            Nom = "Dupond",
                            Prenom = "Joel",
                            DateNaissance = new DateTime(1990, 11, 12),
                            Email = "dupond.joel@gmail.com",
                            Phone = "0678790964"
                        },
                        new Competitor
                        {
                            Id = 2,
                            Nom = "Hateur",
                            Prenom = "Nordine",
                            DateNaissance = new DateTime(1960, 8, 7),
                            Email = "nordinehateur@gmail.com",
                            Phone = "06787965464"
                        },
                        new Competitor
                        {
                            Id = 3,
                            Nom = "Aubert",
                            Prenom = "Jean-Louis",
                            DateNaissance = new DateTime(1990, 11, 12),
                            Email = "jlaubert@gmail.com",
                            Phone = "06787948657"
                        }
                    },
                    Organisers = new List<Organizer>
                    {
                        new Organizer
                        {
                            Id = 4,
                            Nom = "Dupont",
                            Prenom = "toto",
                            DateNaissance = new DateTime(1957, 9, 25),
                            Email = "toto@gmail.com",
                            Phone = "0678154578"
                        }
                    },
                    Pois = null
                },
                new Race
                {
                    Id = 2,
                    Title = "Semi-marathon de Nantes",
                    Description = "Course de 21,1km dans la métropole Nantes",
                    DateStart = new DateTime(2017, 4, 28, 9, 0, 0),
                    DateEnd = new DateTime(2017, 4, 28, 13, 0, 0),
                    Town = "Paris",

                    Competitors = new List<Competitor>
                    {
                        new Competitor
                        {
                            Id = 6,
                            Nom = "Martin",
                            Prenom = "Bernard",
                            DateNaissance = new DateTime(1990, 11, 12),
                            Email = "bebermart1@gmail.com",
                            Phone = "0656320964"
                        },
                        new Competitor
                        {
                            Id = 7,
                            Nom = "Rémy",
                            Prenom = "Marie-Claire",
                            DateNaissance = new DateTime(1973, 6, 4),
                            Email = "mcremy@gmail.com",
                            Phone = "0645102030"
                        }
                    },
                    Organisers = new List<Organizer>
                    {
                        new Organizer
                        {
                            Id = 8,
                            Nom = "Rolland",
                            Prenom = "Johanna",
                            DateNaissance = new DateTime(1987, 1, 3),
                            Email = "johannarlnd@gmail.com",
                            Phone = "0678541020"
                        }
                    },
                    Pois = null
                },
                new Race
                {
                    Id = 3,
                    Title = "Marathon de Nantes",
                    Description = "Course de 42.195km dans la métropole Nantes",
                    DateStart = new DateTime(2017, 4, 28, 14, 0, 0),
                    DateEnd = new DateTime(2017, 4, 28, 20, 0, 0),
                    Town = "Amiens",

                    Competitors = null,
                    Organisers = new List<Organizer>
                    {
                        new Organizer
                        {
                            Id = 6,
                            Nom = "Lievremont",
                            Prenom = "Marc",
                            DateNaissance = new DateTime(1975, 12, 12),
                            Email = "marcolievremont@gmail.com",
                            Phone = "0678548596"
                        }
                    },
                    Pois = null
                },
            };

            return listRace;
        }

        #endregion
    }

}
