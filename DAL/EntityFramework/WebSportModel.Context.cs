
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
    }
}
