using AuthorTest.Areas.Identity.Data;
using AuthorTest.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorTest.Services
{

    //describes the methods that GenomeService must Implement
    public interface IGenomeService
    {
        Task<bool> InitializeDb();
        Task<bool> SeedDb();
        Task<ProjectMembers> UpdateContributor(int ProjectMemberId, string Update);
        Task<Researcher> FindResearcherFromUserId(string Id);
        Task<AuthorTestUser> FindUserByEmail(string Email);
        Task<IdentityRole> FindRoleById(string Id);
        Task<AuthorTestUser> FindUserFromUserId(string Id);
        Task<Project> FindProjectFromProjectId(int Id);
        Task<bool> DeleteProject(int Id);
        Task<ProjectMembers> GetProjectMember(string userId, int projectId);
        Task<ProjectMembers> ChangeProjectMemberStatus(int ProjectMemberId, string status);
        Task<bool> VerifyResearcherStatus(string Id);
        Task<Project> UpdateProject(UpdateProjectDetailsViewModel projectDetailsViewModel);
        Task<Project> UpdateProjectStatus(UpdateProjectStatusViewModel projectstatusViewModel);
        Task<Project> CreateNewProject(ProjectDetailsViewModel projectDetailsViewModel);
        Task<IQueryable<ProjectMembers>> ViewProjectApplicants(int ProjectId);
        Task<ProjectMembers> RejectProjectMember(int ProjectMemberId);
        Task<IQueryable<Project>> ViewProjects(string Id);
        Task<IQueryable<Project>> ViewAllProjects();
        Task<Project> LeaveProject(int projectId, string userId);
        Task<AuthorTestUser> ViewProjectUser(string userId);
        Task<bool> RecruitContributor(ProjectMembers Pm);
        Task<bool> AddUserToProject(string userId, int projectId);
        Task<AuthorTestUser> RegisterUser(RegisterViewModel input);
        Task<AuthorTestUser> RegisterResearcher(ResearcherRegisterViewModel Input);
        Task<bool> SignInUser(SignInViewModel input);
        Task<bool> SignOutUser();
        Task<IQueryable<ProjectMembers>> ViewProjectsContributor(string Id);

    }
}
