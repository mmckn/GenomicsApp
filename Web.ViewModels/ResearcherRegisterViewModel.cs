using System.ComponentModel.DataAnnotations;

namespace AuthorTest.Models
{
    public class ResearcherRegisterViewModel
    {


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string passwordCheck { get; set; }

        [Required]
        [Display(Name = "Organization Name")]
        public string ResearchOrganization { get; set; }

        [Required]
        [Display(Name = "Organization Address")]
        public string ResearchOrganizationAddress { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Organization Email")]
        public string ResearchOrganizationEmail { get; set; }


    }
}
