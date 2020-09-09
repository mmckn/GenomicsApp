using System.ComponentModel.DataAnnotations;

namespace AuthorTest.Models
{
    public class InviteContributorViewModel
    {

        [Required]
        public string UserId { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
