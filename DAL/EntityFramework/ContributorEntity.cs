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
    
    public partial class ContributorEntity
    {
        public int PersonId { get; set; }
        public int RaceId { get; set; }
        public bool IsCompetitor { get; set; }
        public bool IsOrganiser { get; set; }
    
        public virtual RaceEntity Race { get; set; }
        public virtual PersonEntity Person { get; set; }
    }
}