using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorTest.Models
{

    public class EditUserRolesViewModel
    {
        public EditUserRolesViewModel()
        {
            usersWithRoleList = new List<UsersWithRoleViewModel>();
        }

        [Required]
        public string roleName { get; set; }
        public string Id { get; set; }
        public string username { get; set; }
        public List<UsersWithRoleViewModel> usersWithRoleList;

    }
}
