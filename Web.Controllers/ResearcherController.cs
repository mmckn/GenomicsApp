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
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorTest.Controllers
{
    // Handles all the logic of the Researcher role.
    [Authorize(Roles = "Researcher")]
    public class ResearcherController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AuthorTestUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AuthorDbContext authorDbContext;
        private readonly IConfiguration configuration;
        private readonly IGenomeService gsv;
        private readonly IFileService fsv;

        public ResearcherController(IGenomeService genomeService, IFileService fileService, RoleManager<IdentityRole> roleManager, UserManager<AuthorTestUser> userManager, IHttpContextAccessor httpContextAccessor, AuthorDbContext authorDbContext, SignInManager<AuthorTestUser> signInManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.authorDbContext = authorDbContext;
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


        [HttpGet]
        public async Task<IActionResult> EditProject(int ProjectId)
        {
            // Get project with this projectId and update viewmodel with its details
            var project = await gsv.FindProjectFromProjectId(ProjectId);

            var updatedProject = new UpdateProjectDetailsViewModel
            {
                ProjectId = ProjectId,
                Description = project.Description,
                ProjectTitle = project.ProjectTitle,
                RequiredNumberOfParticipants = project.RequiredNumberOfParticipants,
                Email = project.Email,
                ResearchOrganization = project.ResearchOrganization,

            };

            return View(updatedProject);
        }


        [HttpPost]
        public async Task<IActionResult> EditProject(UpdateProjectDetailsViewModel UpdateProject)
        {
            if (ModelState.IsValid)
            {
                //update project in database with new details
                var check = await gsv.UpdateProject(UpdateProject);
                if (check != null)
                {
                    TempData["submission"] = "Project updated successfully! ";
                    TempData["AlertType"] = "alert-success";
                    return RedirectToAction("viewprojects");
                }
            }

            TempData["submission"] = "Project not updated successfully! ";
            TempData["AlertType"] = "alert-danger";
            return RedirectToAction("viewprojects");
        }


        [Breadcrumb("Update Project", FromAction = "ViewProjects")]
        [HttpGet]
        public IActionResult UpdateProjectProgress(int ProjectId)
        {
            var Update = new UpdateProjectStatusViewModel
            {
                ProjectId = ProjectId

            };
            return View(Update);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProjectProgress(UpdateProjectStatusViewModel updatedStatus)
        {
            if (ModelState.IsValid)
            {
                var project = await gsv.FindProjectFromProjectId(updatedStatus.ProjectId);
                if (project != null)
                {

                    // If the project exists then update it.
                    await gsv.UpdateProjectStatus(updatedStatus);


                    TempData["submission"] = "Project updated successfully! ";
                    TempData["AlertType"] = "alert-success";
                    return RedirectToAction("ViewProjects");
                }
            }

            //  If the project does not exist return an error.
            ViewBag.submission = "Project not updated!";
            ViewBag.AlertType = "alert-danger";
            return View(updatedStatus);

        }

        [Breadcrumb("View Projects", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> ViewProjects()
        {
            if (TempData["submission"] != null)
            {
                // If a controller action redirects to this conroller and passes tempdata then assign it to the viewbag, this is uses for passing alerts.
                ViewBag.submission = TempData["submission"].ToString();
                ViewBag.AlertType = TempData["AlertType"].ToString();

            }

            // Get the current users Id.
            var UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var ProjectList = await gsv.ViewProjects(UserId);

            var ProjectListViewModel = new ProjectListViewModel();
            if (ProjectList != null)
            {

                // Add each of the users projects to the list and pass that list to the view.
                foreach (var Project in ProjectList)
                {
                    var ProjectViewModel = new ProjectDetailsViewModel
                    {
                        ProjectTitle = Project.ProjectTitle,
                        Description = Project.Description,
                        Email = Project.Email,
                        RequiredNumberOfParticipants = Project.RequiredNumberOfParticipants,
                        ResearcherId = Project.ResearcherId,
                        ResearchOrganization = Project.ResearchOrganization,
                        CurrentNumberOfParticipants = Project.CurrentNumberOfParticipants,
                        ProgressBar = Project.ProgressBar,
                        ProgressUpdate = Project.Update,
                        ProjectId = Project.Id,

                    };
                    ProjectListViewModel.projectList.Add(ProjectViewModel);

                }
                return View(ProjectListViewModel);
            }

            else
            {
                return View(ProjectListViewModel);
            }
        }



        [Breadcrumb("Review Applicants", FromAction = "ViewProjects")]
        public async Task<IActionResult> ReviewApplicants(int ProjectId)
        {

            //get all the members of this project and pass the list to the view.
            var ProjectApplicants = await gsv.ViewProjectApplicants(ProjectId);


            var ProjectApplicantListViewModel = new ProjectApplicantListViewModel();

            foreach (var Project in ProjectApplicants)
            {
                var ProjectApplicant = new ProjectApplicantViewModel
                {
                    Email = Project.AuthorTestUser.Email,
                    Id = Project.AuthorTestUser.Id,
                    ProjectApplicantId = Project.Id

                };

                if (Project.Applicationstatus == null)
                {
                    ProjectApplicant.status = "Not Assigned";

                }

                else
                {
                    ProjectApplicant.status = Project.Applicationstatus;
                }

                ProjectApplicantListViewModel.projectApplicantList.Add(ProjectApplicant);

            }
            return View(ProjectApplicantListViewModel);

        }


        [Breadcrumb("Update Contributor", FromAction = "ViewProjects")]
        [HttpGet]
        public IActionResult UpdateProjectContributor(int ProjectMemberId)
        {
            var contributorUpdate = new UpdateContributor
            {
                projectMemberId = ProjectMemberId,

            };

            return View(contributorUpdate);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProjectContributor(UpdateContributor Input)
        {
            if (ModelState.IsValid)
            {
                var result = await gsv.UpdateContributor(Input.projectMemberId, Input.update);
                if (result != null)
                {
                    TempData["submission"] = "User updated succesfully!";
                    TempData["AlertType"] = "alert-success";

                    return RedirectToAction("viewprojects");
                }

            }
            ViewBag.submission = "User not updated!";
            ViewBag.AlertType = "alert-danger";

            return View(Input);

        }


        public async Task<IActionResult> AcceptApplicant(int ProjectMemberId)
        {
            string Status = "Accepted";
            await gsv.ChangeProjectMemberStatus(ProjectMemberId, Status);

            TempData["submission"] = "This applicant has been accepted!";
            TempData["AlertType"] = "alert-success";

            return RedirectToAction("ViewProjects");
        }


        public async Task<IActionResult> RejectApplicant(int ProjectMemberId)
        {

            await gsv.RejectProjectMember(ProjectMemberId);
            TempData["submission"] = "This applicant has been rejected!";
            TempData["AlertType"] = "alert-danger";
            return RedirectToAction("ViewProjects");
        }


        [Breadcrumb("Create Project", FromAction = "Index", FromController = typeof(HomeController))]
        [HttpGet]
        public IActionResult CreateProject()
        {
            var projectDetailsViewModel = new ProjectDetailsViewModel();

            return View(projectDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDetailsViewModel projectDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                projectDetailsViewModel.UserId = userId;

                var checkSuccess = await gsv.CreateNewProject(projectDetailsViewModel);

                if (checkSuccess != null)
                {
                    TempData["submission"] = "Project created succesfully!";
                    TempData["AlertType"] = "alert-success";

                    return RedirectToAction("ViewProjects");
                }
            }

            ViewBag.submission = "Project Not Created!";
            ViewBag.AlertType = "alert-danger";

            return View();
        }


        [Breadcrumb("Recruit contributor", FromAction = "ViewProjects")]
        [HttpGet]
        public IActionResult InviteContributorToProject(string ProjectId)
        {
            var InviteContributorModel = new InviteContributorViewModel()
            {
                UserId = ProjectId
            };

            return View(InviteContributorModel);
        }


        [HttpPost]
        public async Task<IActionResult> InviteToProject(InviteContributorViewModel input)

        {
            var User = await userManager.FindByIdAsync(input.UserId);

            var project = await gsv.FindProjectFromProjectId(input.ProjectId);

            if (project != null)
            {

                var projectMember = new ProjectMembers() { Applicationstatus = "Invited", AuthorTestUser = User, Project = project };

                var check = await gsv.RecruitContributor(projectMember);

                if (check)
                {
                    TempData["submission"] = "User has been invited to the project! ";
                    TempData["AlertType"] = "alert-success";

                    return RedirectToAction("index");
                }
            }
            
            
                TempData["submission"] = "User has been not been invited to the project! check the Project Id is correct. ";
                TempData["AlertType"] = "alert-danger";

                return RedirectToAction("index");
            
        }




        public async Task<IActionResult> DeleteProject(int projectId)
        {
            //check if the information(model) recieved is correct/right format.
            var check = await gsv.DeleteProject(projectId);

            if (check)
            {
                TempData["submission"] = "Project succesfully deleted! ";
                TempData["AlertType"] = "alert-success";

                return RedirectToAction("viewprojects");
            }
            else
            {
                TempData["submission"] = "Project note deleted try again!";
                TempData["AlertType"] = "alert-danger";

                return RedirectToAction("viewprojects");
            }


        }


        [Breadcrumb("Contributor Genome", FromAction = "ViewProjects")]
        public async Task<IActionResult> ContributorGenomes(string contributorId)
        {
            var file = await fsv.GetGenome(contributorId);

            if (file == null)
            {
                TempData["submission"] = "This user has not uploaded their genome yet!";
                TempData["AlertType"] = "alert-danger";
                return RedirectToAction("ViewProjects");

            }

            return View(file);

        }


        public async Task<IActionResult> DownloadGenome(string genomeName)
        {
            var blockBlob = await fsv.DownloadGenome(genomeName);

            Stream blobStream = blockBlob.OpenReadAsync().Result;

            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
        }


    }


}

