using AuthorTest.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthorTest.Data
{ 
    // Db context extends the Identity API adding addtional tables to the Identity SQL database.
    public class AuthorDbContext : IdentityDbContext<AuthorTestUser>
    {

        public AuthorDbContext(DbContextOptions<AuthorDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            // Fluent api to assign one to one relationship/foreign keys between researcher and authortestuser(the user type shared by all roles).
            builder.Entity<AuthorTestUser>()
         .HasOne<Researcher>(a => a.Researcher)
         .WithOne(r => r.AuthorTestUser)
         .HasForeignKey<Researcher>(a => a.AuthorTestUserId)
         .IsRequired(false)
        .OnDelete(DeleteBehavior.Cascade);
        }

        // Initialise ResearcherInfo which stores Researcher Information it is linked by a foreign key in a 1 to 1/0 relationship with the IdentityUserTable.
        public DbSet<Researcher> ResearcherInfo { get; set; }

        public DbSet<Project> ProjectInfo { get; set; }

        public DbSet<ProjectMembers> projectMembers { get; set; }



    }
}
