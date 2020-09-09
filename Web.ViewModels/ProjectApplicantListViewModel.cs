using System.Collections.Generic;

namespace AuthorTest.Models
{

    public class ProjectApplicantListViewModel
    {
        public ProjectApplicantListViewModel()
        {
            projectApplicantList = new List<ProjectApplicantViewModel>();
        }

        public List<ProjectApplicantViewModel> projectApplicantList;

    }
}
