using System.ComponentModel.DataAnnotations;
//provides a blueprint for creating new roles
namespace AuthorTest.Models
{
    public class UpdateContributor
    {
        [Required]
        public string update { get; set; }

        [Required]
        public int projectMemberId { get; set; }

    }
}
