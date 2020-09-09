using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorTest.Areas.Identity.Data
{
    // This class sets out the information each researcher needs to be verified.
    public class Researcher
    {
        public Researcher()
        {
            Projects = new List<Project>();
        }

        public int Id { get; set; }

        [PersonalData]
        //sets the datatype in the table
        [Column(TypeName = "nvarchar(90)")]
        public string ResearcherRole { get; set; }

        [PersonalData]

        [Column(TypeName = "nvarchar(90)")]
        public string ResearchOrganization { get; set; }

        [Column(TypeName = "nvarchar(90)")]
        public string ResearchOrganizationAddress { get; set; }

        [Column(TypeName = "nvarchar(90)")]
        public string ResearchOrganizationEmail { get; set; }

        public string AuthorTestUserId { get; set; }

        public AuthorTestUser AuthorTestUser { get; set; }

        public ICollection<Project> Projects { get; set; }

    }
}
