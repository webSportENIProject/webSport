//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class ParticipantEntity
    {
        public int PersonneId { get; set; }
        public int CourseId { get; set; }
        public bool EstCompetiteur { get; set; }
        public bool EstOrganisateur { get; set; }
        public System.DateTime dateInscription { get; set; }
        public int dossard { get; set; }
    
        public virtual CourseEntity Course { get; set; }
        public virtual PersonneEntity Personne { get; set; }
    }
}
