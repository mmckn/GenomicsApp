//provides a blueprint for creating new roles
namespace AuthorTest.Models
{
    public class ResearcherDetailsViewMOdel
    {//Role name is the only required feature to create a new role
        public ResearcherDetailsViewMOdel()
        {

        }

        public string ResearcherOrganization { get; set; }
        public string ResearcherStatus { get; set; }
        public string ResearcherEmail { get; set; }
        public string ResearcherId { get; set; }
        public string ResearcherAddress { get; set; }
    }
}
