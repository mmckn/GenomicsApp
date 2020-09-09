using Microsoft.AspNetCore.Identity;
namespace AuthorTest.Areas.Identity.Data
{
    // Extends the stanadard user created by the Identity API and adds a foreign key to the researcher table.
    public class AuthorTestUser : IdentityUser
    {
        public AuthorTestUser()
        {
            Researcher = new Researcher();
        }

        public Researcher Researcher { get; set; }

    }
}
