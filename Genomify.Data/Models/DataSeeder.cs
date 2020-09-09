using AuthorTest.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AuthorTest.Data
{ // Seeds the database with roles and a user login for each role.
    public class DataSeeder
    {
        public async Task<bool> DataSeed(UserManager<AuthorTestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateRole(roleManager, "Researcher");
            await CreateRole(roleManager, "Admin");
            await CreateRole(roleManager, "Contributor");
            var r1 = CreateResearcher("Verified", "Glasgow University", "random@gla.ac.uk", "25 Finnieston road");
            await CreateUser(userManager, "researcher@aol.com", "Michael", r1, "Researcher");
            await CreateUser(userManager, "contributor@aol.com", "John", "Contributor");
            await CreateUser(userManager, "admin@aol.com", "John", "Admin");
            return true;

        }

        public Researcher CreateResearcher(string researcherRole, string organization, string email, string address)
        {
            var researcher = new Researcher
            {

                ResearcherRole = researcherRole,
                ResearchOrganization = organization,
                ResearchOrganizationEmail = email,
                ResearchOrganizationAddress = address

            };

            return researcher;


        }

        public async Task<bool> CreateUser(UserManager<AuthorTestUser> userManager, string email, string firstName, Researcher researcher, string role)
        {
            var check = await userManager.FindByEmailAsync(email);
            if (check == null)
            {
                var user = new AuthorTestUser
                {

                    Email = email,
                    UserName = email,

                    Researcher = researcher
                };

                await userManager.CreateAsync(user, "password");
                await userManager.AddToRoleAsync(user, role);

                return true;
            }
            return false;
        }


        public async Task<bool> CreateUser(UserManager<AuthorTestUser> userManager, string email, string firstName, string role)
        {
            var check = await userManager.FindByEmailAsync(email);
            if (check == null)
            {
                var user = new AuthorTestUser
                {
                    Email = email,
                    UserName = email
                };

                await userManager.CreateAsync(user, "password");
                await userManager.AddToRoleAsync(user, role);
            }

            return true;
        }




        public async Task<bool> CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var check = await roleManager.RoleExistsAsync(roleName);

            if (check)
            {

            }
            else
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await roleManager.CreateAsync(role);
            }

            return true;
        }



    }
}
