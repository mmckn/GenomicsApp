using System.ComponentModel.DataAnnotations;
//provides a blueprint for creating new roles
namespace AuthorTest.Models
{
    public class UpdateProjectStatusViewModel
    {
        public UpdateProjectStatusViewModel()
        {
            
        }

        [Display(Name = "Project update")]
        public string ProjectUpdate { get; set; }

        [Display(Name = "Project progress bar"), Range(0, 100,
        ErrorMessage = "Value must be between 0 and 100.")]
        public int ProjectProgress { get; set; }

        public int ProjectId { get; set; }
      


    }
}
