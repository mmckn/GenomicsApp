using AuthorTest.Areas.Identity.Data;
using AuthorTest.Data;
using AuthorTest.Models;
using AuthorTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorTest.Controllers
{  
    // Handles all the logic of the admin role.

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AuthorTestUser> userManager;
        private readonly AuthorDbContext authorDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<AuthorTestUser> signInManager;
        private readonly IGenomeService gsv;

        public AdminController(IGenomeService genomeService, RoleManager<IdentityRole> roleManager, UserManager<AuthorTestUser> userManager, AuthorDbContext authorDbContext, IHttpContextAccessor httpContextAccessor, SignInManager<AuthorTestUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.authorDbContext = authorDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
            gsv = genomeService;
        }


        public IActionResult Index()
        {
            if (TempData["submission"] != null)
            {
                ViewBag.submission = TempData["submission"].ToString();
                ViewBag.AlertType = TempData["AlertType"].ToString();

                return RedirectToAction("index", "home");
            }
            return RedirectToAction("index", "home");
        }


        [Breadcrumb("Researcher Status", FromAction = "ManageResearchers")]
        [HttpGet]
        public async Task<IActionResult> ReviewResearcherStatus(string Id)
        {
            //gets researcher details from table and passes them to view
            var researcherInfo = await gsv.FindResearcherFromUserId(Id);

            var researcherDetails = new ResearcherDetailsViewMOdel
            {

                ResearcherOrganization = researcherInfo.ResearchOrganization,
                ResearcherStatus = researcherInfo.ResearcherRole,
                ResearcherAddress = researcherInfo.ResearchOrganizationAddress,
                ResearcherEmail = researcherInfo.ResearchOrganizationEmail,
                ResearcherId = researcherInfo.AuthorTestUserId,
            };

            return View(researcherDetails);
        }


        [HttpGet]
        public async Task<IActionResult> VerifyResearcherStatus(string Id)
        {
            //changes the Researchers status to verified
            var check = await gsv.VerifyResearcherStatus(Id);

            if (check == true)
            {
                TempData["submission"] = "User succesfully verified!";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("ManageResearchers");
            }

            else
            {
                TempData["submission"] = "User not verified!";
                TempData["AlertType"] = "alert-danger";
                return RedirectToAction("ManageResearchers");
            }

        }



        [Breadcrumb("Manage Researchers", FromAction = "Index", FromController = typeof(HomeController))]
        [HttpGet]
        public async Task<IActionResult> ManageResearchers()
        {

            if (TempData["submission"] != null)
            {
                ViewBag.submission = TempData["submission"].ToString();
                ViewBag.AlertType = TempData["AlertType"].ToString();
            }

            var usersOfRole = await userManager.GetUsersInRoleAsync("Researcher");

            // Creates a viewmodel and assigns the username and researcherstatus  to the usersWithRoleList IList and the role name to the rolename.
            var viewModel = new EditUserRolesViewModel();


            foreach (var user in usersOfRole)
            {
                // Creates a viewmodel and assigns the username and researcherstatus and adds the viewmodel to the usersWithRoleList IList of viewModelList.
                var userDetails = new UsersWithRoleViewModel();
                userDetails.username = user.Email;
                userDetails.Id = user.Id;

                var ResearcherExists = await gsv.FindResearcherFromUserId(user.Id);

                if (ResearcherExists != null)
                {
                    // Assign only if this user has applied to be a researcher/has a research table.
                    userDetails.status = ResearcherExists.ResearcherRole;
                }

                else { userDetails.status = "Not Set"; }

                viewModel.usersWithRoleList.Add(userDetails);
            }

            viewModel.roleName = "Researcher";
            return View(viewModel);
        }



    }


}
