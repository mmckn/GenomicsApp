namespace AuthorTest.Models
{// this viewModel is used to store the details from the UserRole table which contains the id(foreign keys) for the user and role tables for a user
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {

        }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Username { get; set; }
        
    }
}
