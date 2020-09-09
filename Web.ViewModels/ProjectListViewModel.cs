using System.Collections.Generic;
//provides a blueprint for creating new roles
namespace AuthorTest.Models
{
    public class ProjectListViewModel
    {//Role name is the only required feature to create a new role
        public ProjectListViewModel()
        {
            projectList = new List<ProjectDetailsViewModel>();
        }

        public List<ProjectDetailsViewModel> projectList;
    }
}
