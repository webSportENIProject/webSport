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
    
    public partial class PersonneEntity
    {
        public PersonneEntity()
        {
            this.Participant = new HashSet<ParticipantEntity>();
        }
    
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Nullable<System.DateTime> DateNaissance { get; set; }
        public Nullable<int> UserTableId { get; set; }
        public bool kms { get; set; }
        public bool miles { get; set; }
    
        public virtual ICollection<ParticipantEntity> Participant { get; set; }
        public virtual UserTable UserTable { get; set; }
    }
}
