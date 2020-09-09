using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorTest.Areas.Identity.Data
{

    // Add profile data for application users by adding properties to the AuthorTestUser class.
    // This class gives the information for the projectinfo table (see AuthorDbContext.cs).

    public class Project
    {


        public int Id { get; set; }


        //sets the datatype in the table
        [Column(TypeName = "nvarchar(90)")]

        public string ProjectTitle { get; set; }

        [Column(TypeName = "nvarchar(90)")]
        public string ResearchOrganization { get; set; }


        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }


        [Column(TypeName = "nvarchar(90)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(90)")]
        //changed from int to string
        public string Status { get; set; }

        [Column(TypeName = "int")]
        public int RequiredNumberOfParticipants { get; set; }

        [Column(TypeName = "int")]
        public int CurrentNumberOfParticipants { get; set; }

        [Column(TypeName = "int")]
        public int ProgressBar { get; set; }

        [Column(TypeName = "nvarchar(90)")]

        public string Update { get; set; }

        [ForeignKey("Researcher")]
        public int ResearcherId { get; set; }

        public Researcher Researcher { get; set; }







    }
}
