﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebSportEntities : DbContext
    {
        public WebSportEntities()
            : base("name=WebSportEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CourseEntity> Course { get; set; }
        public DbSet<ParticipantEntity> Participant { get; set; }
        public DbSet<PersonneEntity> Personne { get; set; }
        public DbSet<UserTable> UserTable { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
        public DbSet<MailEntity> Mail { get; set; }
        public DbSet<PointEntity> Point { get; set; }
        public DbSet<TypePointEntity> TypePoint { get; set; }
        public DbSet<ResultatEntity> Resultat { get; set; }
    }
}
