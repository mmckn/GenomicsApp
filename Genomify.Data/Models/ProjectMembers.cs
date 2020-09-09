namespace AuthorTest.Areas.Identity.Data
{
   //  This is the class that implements a joining table allowing contributors to join a project and recieve personal updates.
    public class ProjectMembers
    {
        public int Id { get; set; }
        public string Applicationstatus { get; set; }
        public string ProjectUpdate { get; set; }
        public AuthorTestUser AuthorTestUser { get; set; }
        public Project Project { get; set; }

    }
}
