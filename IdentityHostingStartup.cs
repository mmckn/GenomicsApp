using AuthorTest.Areas.Identity.Data;
using AuthorTest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



 // This class manages the Identity API settings adding them in addition to the startup services.


[assembly: HostingStartup(typeof(AuthorTest.Areas.Identity.IdentityHostingStartup))]
namespace AuthorTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AuthorDbContext>(options =>
                    options.UseSqlServer(

                        // Change string to "AuthorDbContextConnctionProd" for Azure SQL Db, remove "prod" for local Db".
                        context.Configuration.GetConnectionString("AuthorDbContextConnection")));

                services.BuildServiceProvider().GetService<AuthorDbContext>().Database.Migrate();

                // Use options to change identity signin requirements.
                services.AddDefaultIdentity<AuthorTestUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.SignIn.RequireConfirmedAccount = false;
                })


                
                // Enables use of role manager.
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthorDbContext>();

                services.ConfigureApplicationCookie(options =>
                {
                    // Manage views user gets when trying to access an area they are not allowed to.
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.LoginPath = "/UserAccount/UserRegistration";
                }); 


            });
          
        }
    }
}