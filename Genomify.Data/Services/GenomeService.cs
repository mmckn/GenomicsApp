using AuthorTest.Areas.Identity.Data;
using AuthorTest.Data;
using AuthorTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorTest.Services
{


    // This file handles all interaction between the SQL database and the web application.
    public class GenomeService : IGenomeService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AuthorTestUser> userManager;
        private readonly AuthorDbContext authorDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<AuthorTestUser> signInManager;
        private readonly DataSeeder dataSeeder;


        public GenomeService(RoleManager<IdentityRole> roleManager, UserManager<AuthorTestUser> userManager, IHttpContextAccessor httpContextAccessor, AuthorDbContext authorDbContext, SignInManager<AuthorTestUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.authorDbContext = authorDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
            dataSeeder = new DataSeeder();

        }


        public async Task<bool> InitializeDb()
        {
            // Delete and recreate the database with any migrations
            await authorDbContext.Database.EnsureDeletedAsync();
         
            // Update the database with any migrations
            await authorDbContext.Database.MigrateAsync();
            await authorDbContext.SaveChangesAsync();
            await dataSeeder.DataSeed(userManager, roleManager);

            return true;
        }

        public async Task<bool> SeedDb()
        {
            // Seed the database with users and roles.
            await dataSeeder.DataSeed(userManager, roleManager);
            return true;
        }




        public async Task<AuthorTestUser> FindUserByEmail(string Email)
        {
            var x = await userManager.FindByEmailAsync(Email);
            return x;

        }




        public async Task<AuthorTestUser> FindUserFromUserId(string Id)
        {
            var User = await userManager.FindByIdAsync(Id);

            if (User == null)
            {
                return null;
            }

            // Recieves an Identity user Id and uses this to find the corresponding researcher information.
            return User;
        }



        public async Task<IdentityRole> FindRoleById(string Id)
        {
            var Role = await roleManager.FindByIdAsync(Id);

            // Check the role exists
            if (Role != null)
            {
                return Role;
            }
            else
            {
                return null;
            }
        }




        //  <----------------------- Researcher ------------------------->



        public async Task<Project> FindProjectFromProjectId(int Id)
        {
            var Project = await authorDbContext.ProjectInfo.FirstOrDefaultAsync(p => p.Id == Id);

            if (Project != null)
            {
                return Project;
            }

            {
                return null;
            }
        }



        public async Task<Researcher> FindResearcherFromUserId(string Id)
        {
            //recieves an Identity user Id and uses this to find the corresponding researcher Information
            var ResearcherInfo = await authorDbContext.ResearcherInfo.FirstOrDefaultAsync(v => v.AuthorTestUserId.Equals(Id));
            if (ResearcherInfo != null)
            {
                return ResearcherInfo;
            }

            {
                return null;
            }
        }


        public async Task<Project> CreateNewProject(ProjectDetailsViewModel projectDetailsViewModel)
        {

            var researcher = await FindResearcherFromUserId(projectDetailsViewModel.UserId);

            var project = new Project
            {
                ProjectTitle = projectDetailsViewModel.ProjectTitle,
                Description = projectDetailsViewModel.Description,
                Email = projectDetailsViewModel.Email,
                RequiredNumberOfParticipants = projectDetailsViewModel.RequiredNumberOfParticipants,
                ResearchOrganization = projectDetailsViewModel.ResearchOrganization,
            };

            researcher.Projects.Add(project);

            await authorDbContext.SaveChangesAsync();

            // Check the project has been added to the researchers project list
            if (project.Email != null)
            {
                var check = researcher.Projects.FirstOrDefault(p => p.Email.Equals(project.Email));
                return check;
            }
            else
            {
                return null;
            }
        }


        public async Task<Project> UpdateProject(UpdateProjectDetailsViewModel projectDetailsViewModel)
        { //Check the project exists, if it does then update it.

            var project = await FindProjectFromProjectId(projectDetailsViewModel.ProjectId);

            if (project != null)
            {

                project.ProjectTitle = projectDetailsViewModel.ProjectTitle;
                project.Description = projectDetailsViewModel.Description;
                project.Email = projectDetailsViewModel.Email;
                project.RequiredNumberOfParticipants = projectDetailsViewModel.RequiredNumberOfParticipants;
                project.ResearchOrganization = projectDetailsViewModel.ResearchOrganization;
                await authorDbContext.SaveChangesAsync();

                if (project.Email != null)
                {
                    var check = authorDbContext.ProjectInfo.FirstOrDefault(p => p.Email.Equals(project.Email));
                    return check;
                }
            }


            return null;

        }


        public async Task<Project> UpdateProjectStatus(UpdateProjectStatusViewModel projectstatusViewModel)
        {
            // Get the project if it is found then update it.
            var project = await FindProjectFromProjectId(projectstatusViewModel.ProjectId);

            if (project != null)
            {

                project.Update = projectstatusViewModel.ProjectUpdate;
                project.ProgressBar = projectstatusViewModel.ProjectProgress;
                await authorDbContext.SaveChangesAsync();

                return project;
            }

            return null;
        }


        public async Task<Project> LeaveProject(int projectId, string userId)
        {
            //Check the user is a member of the project if they are then remove them. Check the user has been removed If this is successful then reduce project participant count by 1. 
            var projectMember = await GetProjectMember(userId, projectId);
            if (projectMember != null)
            {
                authorDbContext.projectMembers.Remove(projectMember);
                await authorDbContext.SaveChangesAsync();

                var CheckRemoved = await authorDbContext.projectMembers.FindAsync(projectMember.Id);

                if (CheckRemoved != null)
                {
                    return null;
                }
                else
                {
                    var project = await FindProjectFromProjectId(projectId);
                    project.CurrentNumberOfParticipants--;
                    await authorDbContext.SaveChangesAsync();

                    return project;
                }
            }
            return null;
        }



        public async Task<ProjectMembers> UpdateContributor(int ProjectMemberId, string Update)
        {
            var projectmember = await authorDbContext.projectMembers.FindAsync(ProjectMemberId);
            projectmember.ProjectUpdate = Update;
            await authorDbContext.SaveChangesAsync();

            return projectmember;

        }
        public async Task<IQueryable<Project>> ViewProjects(string Id)
        {
            var Researcher = await FindResearcherFromUserId(Id);
            if (Researcher != null)
            {
                var Projects = authorDbContext.ProjectInfo.Where(p => p.ResearcherId == Researcher.Id);

                if (await Projects.CountAsync() != 0)
                {
                    return Projects;
                }
            }

            return null;

        }

        public async Task<ProjectMembers> ChangeProjectMemberStatus(int ProjectMemberId, string Status)
        {
            var ProjectMember = await authorDbContext.projectMembers.FindAsync(ProjectMemberId);
            if (ProjectMember == null)
            {
                return null;
            }

            ProjectMember.Applicationstatus = Status;
            await authorDbContext.SaveChangesAsync();

            return ProjectMember;
        }


        public async Task<ProjectMembers> RejectProjectMember(int ProjectMemberId)
        {
            var ProjectMember = await authorDbContext.projectMembers.FindAsync(ProjectMemberId);

            var ProjectMemberWithProject = await authorDbContext.projectMembers.Include("Project").FirstOrDefaultAsync(pm => pm.Id == ProjectMember.Id);

            if (ProjectMemberWithProject == null)
            {
                return null;
            }

            ProjectMemberWithProject.Project.CurrentNumberOfParticipants--;
            authorDbContext.projectMembers.Remove(ProjectMember);
            await authorDbContext.SaveChangesAsync();

            return ProjectMember;
        }


        public async Task<IQueryable<ProjectMembers>> ViewProjectApplicants(int ProjectId)
        {
            var Project = await FindProjectFromProjectId(ProjectId);

            if (Project == null)
            {
                return null;
            }

            var projectMemberList = authorDbContext.projectMembers.Include("AuthorTestUser").Where(pm => pm.Project == Project);


            return projectMemberList;
        }


        public async Task<bool> DeleteProject(int Id)
        {
            var foundProject = await authorDbContext.ProjectInfo.FindAsync(Id);


            if (foundProject != null)
            {
                foreach (var v in authorDbContext.projectMembers)
                {
                    if (v.Project == foundProject)
                    {
                        authorDbContext.projectMembers.Remove(v);
                    }
                }


                var result = authorDbContext.ProjectInfo.Remove(foundProject);
                await authorDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }


        public async Task<ProjectMembers> GetProjectMember(string userId, int projectId)
        {

            var User = await FindUserFromUserId(userId);

            var ProjectMember = await authorDbContext.projectMembers.FirstOrDefaultAsync(p => p.AuthorTestUser.Id.Equals(userId) && p.Project.Id == projectId);

            return ProjectMember;
        }


        public async Task<AuthorTestUser> ViewProjectUser(string userId)
        {
            var user = await FindUserFromUserId(userId);

            return user;
        }


        //<-------------------------- Contributors ---------------------->


        public async Task<bool> AddUserToProject(string userId, int projectId)
        {
            // Get the user and the project, create a projectmember for them and add it the the database.
            var user = await FindUserFromUserId(userId);
            var project = await FindProjectFromProjectId(projectId);
            var projectMember = await GetProjectMember(userId, projectId);

            // Check if user has been invited to project, if they have then change their status to accepted, need to check if null to prevent null reference exception.
            if (projectMember != null && projectMember.Applicationstatus.ToLower().Equals("invited"))
            {
                projectMember.Applicationstatus = "Accepted";
                await authorDbContext.SaveChangesAsync();
                return true;
            }

            // Ensure user and project exist
            if (user != null && project != null)
            {
                var projectMemberNew = new ProjectMembers { AuthorTestUser = user, Project = project, Applicationstatus = "Pending Review", ProjectUpdate = "No updates currently." };

                //update the projects participant count
                project.CurrentNumberOfParticipants += 1;

                var checkAdded = await authorDbContext.projectMembers.AddAsync(projectMemberNew);

                await authorDbContext.SaveChangesAsync();

                if (checkAdded == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }



        public async Task<IQueryable<ProjectMembers>> ViewProjectsContributor(string Id)
        {
            //pass in user Id use this to find instances of user
            var user = await FindUserFromUserId(Id);
            var x = authorDbContext.projectMembers.Include("Project").Where(p => p.AuthorTestUser == user);

            return x;

        }


        public async Task<IQueryable<Project>> ViewAllProjects()
        {

            var Projects = authorDbContext.ProjectInfo.Include("Researcher").Where(p => p.CurrentNumberOfParticipants < p.RequiredNumberOfParticipants);

            await Projects.ToListAsync();


            if (await Projects.CountAsync() == 0)
            {
                return null;
            }

            return Projects;

        }


        public async Task<bool> RecruitContributor(ProjectMembers Pm)
        {
            await authorDbContext.projectMembers.AddAsync(Pm);

            await authorDbContext.SaveChangesAsync();

            var checkAdded = await authorDbContext.projectMembers.Include("Project").FirstOrDefaultAsync(p => p.Project.Id == Pm.Project.Id);
            checkAdded.Project.CurrentNumberOfParticipants++;

            if (checkAdded == null)
            {
                return false;
            }

            else
            {
                return true;
            }


        }

        //<-------------------------Account Management ------------------------>

        public async Task<AuthorTestUser> RegisterResearcher(ResearcherRegisterViewModel Input)
        {
            var User = new AuthorTestUser
            {
                UserName = Input.Email,
                Email = Input.Email,
            };

            var CheckExists = await FindUserByEmail(User.Email);
            if (CheckExists == null)
            {
                //add the user to our database
                var result = await userManager.CreateAsync(User, Input.Password);

                //make sure researcher role exists if it doesn't create it.
                if (await roleManager.RoleExistsAsync("Researcher") != true)
                {
                    var researcher = new IdentityRole { Name = "Researcher" };
                    await roleManager.CreateAsync(researcher);
                }
                await userManager.AddToRoleAsync(User, "Researcher");



                var ResearcherInformation = await authorDbContext.ResearcherInfo.FirstOrDefaultAsync(v => v.AuthorTestUserId == User.Id);
                ResearcherInformation.ResearcherRole = "Pending Review";
                ResearcherInformation.ResearchOrganization = Input.ResearchOrganization;
                ResearcherInformation.ResearchOrganizationAddress = Input.ResearchOrganizationAddress;
                ResearcherInformation.ResearchOrganizationEmail = Input.ResearchOrganizationEmail;
                authorDbContext.SaveChanges();


                if (result.Succeeded)
                {
                    // if user is added succesfully sign them in, using a non persistant cookie

                    return User;
                }
            }

            return null;
        }


        public async Task<AuthorTestUser> RegisterUser(RegisterViewModel Input)
        {
            //create a new user
            var User = new AuthorTestUser { UserName = Input.Email, Email = Input.Email };
            //add the user to our database
            var Result = await userManager.CreateAsync(User, Input.Password);

            if (Result.Succeeded)
            {
                var UserToAdd = await userManager.FindByEmailAsync(User.Email);

                var RoleExists = await roleManager.RoleExistsAsync("Contributor");

                if (RoleExists)
                {

                    await userManager.AddToRoleAsync(UserToAdd, "Contributor");
                }
                else
                {
                    var ContributorRole = new IdentityRole { Name = "Contributor" };
                    await roleManager.CreateAsync(ContributorRole);
                    await userManager.AddToRoleAsync(UserToAdd, "Contributor");
                }


                return User;
            }
            return null;
        }


        public async Task<bool> SignInUser(SignInViewModel input)
        {
            var Result = await signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);
            if (Result.Succeeded)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> SignOutUser()
        {
            await signInManager.SignOutAsync();

            return true;
        }



        // <-------------------------------- Admin ------------------------------------------------->

        public async Task<bool> VerifyResearcherStatus(string Id)
        {
            var ResearcherInfo = await FindResearcherFromUserId(Id);

            // Check the reseaarcher exists
            if (ResearcherInfo != null)
            {
                ResearcherInfo.ResearcherRole = "Verified";
                await authorDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
