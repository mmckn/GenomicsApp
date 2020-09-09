using System.ComponentModel.DataAnnotations;


namespace AuthorTest.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
