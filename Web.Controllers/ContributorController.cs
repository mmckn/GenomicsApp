using AuthorTest.Areas.Identity.Data;
using AuthorTest.Data;
using AuthorTest.Models;
using AuthorTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartBreadcrumbs.Attributes;
using System.Security.Claims;
using System.Threading.Tasks;



namespace AuthorTest.Controllers
{
    // Handles all the logic of the Contributor role.
    [Authorize(Roles = "Contributor")]
    public class ContributorController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AuthorTestUser> userManager;
        private readonly AuthorDbContext authorDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<AuthorTestUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IGenomeService gsv;
        private readonly IFileService fsv;


        public ContributorController(IGenomeService genomeService, IFileService fileService, RoleManager<IdentityRole> roleManager, UserManager<AuthorTestUser> userManager, AuthorDbContext authorDbContext, IHttpContextAccessor httpContextAccessor, SignInManager<AuthorTestUser> signInManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.authorDbContext = authorDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
            this.configuration = configuration;
            gsv = genomeService;
            fsv = fileService;

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


        [Breadcrumb("Join projects", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> ViewAllProjects()
        {
         
            var projectList = await gsv.ViewAllProjects();
            var projectListViewModel = new ProjectListViewModel();

            if (projectList != null)
            {
                foreach (var project in projectList)
                {
                    if (project.Researcher.ResearcherRole.Equals("Verified"))
                    {
                        var projectViewModel = new ProjectDetailsViewModel
                        {
                            ProjectTitle = project.ProjectTitle,
                            Description = project.Description,
                            Email = project.Email,
                            RequiredNumberOfParticipants = project.RequiredNumberOfParticipants,
                            ResearcherId = project.ResearcherId,
                            ResearchOrganization = project.ResearchOrganization,
                            ProjectId = project.Id,

                        };
                        projectListViewModel.projectList.Add(projectViewModel);
                    }
                }
            }


            return View(projectListViewModel);
        }


        [Breadcrumb("My Projects", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> MyProjects()

        {
            if (TempData["submission"] != null)
            {
                ViewBag.submission = TempData["submission"].ToString();
                ViewBag.AlertType = TempData["AlertType"].ToString();
            }

            var contributorId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userList = await gsv.ViewProjectsContributor(contributorId);
            var projectListViewModel = new ProjectListViewModel();

            foreach (var user in userList)
            {
                var projectViewModel = new ProjectDetailsViewModel
                {
                    ProjectTitle = user.Project.ProjectTitle,
                    Description = user.Project.Description,
                    Email = user.Project.Email,
                    RequiredNumberOfParticipants = user.Project.RequiredNumberOfParticipants,
                    ResearcherId = user.Project.ResearcherId,
                    ResearchOrganization = user.Project.ResearchOrganization,
                    ProgressBar = user.Project.ProgressBar,
                    ProgressUpdate = user.Project.Update,
                    ApplicationStatus = user.Applicationstatus,
                    ProjectId = user.Project.Id,
                    UserUpdate = user.ProjectUpdate,
                };

                projectListViewModel.projectList.Add(projectViewModel);
            }

            return View(projectListViewModel);
        }

        public async Task<IActionResult> JoinProject(int projectId)
        {

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var checkIfMember = await gsv.GetProjectMember(userId, projectId);
            if (checkIfMember != null && !checkIfMember.Applicationstatus.ToLower().Equals("invited")) 
            {
              
                TempData["submission"] = "You are already a member of this project!";
                TempData["AlertType"] = "alert-danger";
                return RedirectToAction("myprojects");
            }

            await gsv.AddUserToProject(userId, projectId);
            TempData["submission"] = "You are now a member of this project!";
            TempData["AlertType"] = "alert-success";
            return RedirectToAction("myprojects");

        }


        public async Task<IActionResult> LeaveProject(int projectId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var success = await gsv.LeaveProject(projectId, userId);

            if (success != null)
            {
                TempData["submission"] = "You have left the project!";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("myprojects");
            }

            TempData["submission"] = "You have not left the project!";
            TempData["AlertType"] = "alert-danger";
            return RedirectToAction("myprojects");
        }


        //allows upload of any size of file
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadGenome(IFormFile file)
        {
            if (file != null)
            {
                var uploadCheck = await fsv.UploadGenome(file);

                if (uploadCheck)
                {
                    TempData["submission"] = "Genome Successfully uploaded! ";
                    TempData["AlertType"] = "alert-success";
                    return RedirectToAction("mygenome");
                }

                TempData["submission"] = "Genome upload failed! File extension should be .fasta or .vcf. ";
                TempData["AlertType"] = "alert-danger";
                return RedirectToAction("mygenome");
            }

            TempData["submission"] = "Please select a file.";
            TempData["AlertType"] = "alert-danger";
            return RedirectToAction("mygenome");
        }


        public async Task<IActionResult> DeleteGenome(string genomeName)
        {
            var checkDeleted = await fsv.DeleteGenome(genomeName);
            if (checkDeleted)
            {
                TempData["submission"] = "Genome Successfully Deleted! ";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("mygenome");
            }

            TempData["submission"] = "File deletion failed! ";
            TempData["AlertType"] = "alert-danger";
            return RedirectToAction("mygenome");
        }


        [Breadcrumb("My Genome", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> MyGenome()
        {
            if (TempData["submission"] != null)
            {
                ViewBag.submission = TempData["submission"].ToString();
                ViewBag.AlertType = TempData["AlertType"].ToString();
            }

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var genomeFile = await fsv.GetGenome(userId);
            return View(genomeFile);
        }



    }
}
