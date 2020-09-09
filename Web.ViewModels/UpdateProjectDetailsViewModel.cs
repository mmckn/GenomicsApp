using System.ComponentModel.DataAnnotations;
//provides a blueprint for creating new roles
namespace AuthorTest.Models
{
    public class UpdateProjectDetailsViewModel
    {
        public UpdateProjectDetailsViewModel()
        {
            
        }

        [Display(Name = "Project Title")]
        [Required]
        public string ProjectTitle { get; set; }
       
        [Required]
        [Display(Name = "Research Organization")]
        public string ResearchOrganization { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        
        [Display(Name = "Required number of participants"), Range(0, 10000,
        ErrorMessage = "Value must be between 0 and 100000.")]
        public int RequiredNumberOfParticipants { get; set; }

       
        public int ProjectId { get; set; }
   

    }
}
