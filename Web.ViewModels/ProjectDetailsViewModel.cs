using System.ComponentModel.DataAnnotations;
//provides a blueprint for creating new roles
namespace AuthorTest.Models
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel()
        {

        }

        [Display(Name = "Project Title")]
        [Required]
        public string ProjectTitle { get; set; }

        public string ApplicationStatus { get; set; }

        [Required]
        [Display(Name = "Research Organization")]
        public string ResearchOrganization { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string UserUpdate { get; set; }

        public int CurrentNumberOfParticipants { get; set; }

        [Required]
        [Display(Name = "Required number of participants"), Range(0, 10000,
        ErrorMessage = "Value must be between 0 and 10000.")]
        public int RequiredNumberOfParticipants { get; set; }

        public int ProgressBar { get; set; }
        public string ProgressUpdate { get; set; }

        public int ProjectId { get; set; }
        public string UserId { get; set; }

        public int ResearcherId { get; set; }


    }
}
