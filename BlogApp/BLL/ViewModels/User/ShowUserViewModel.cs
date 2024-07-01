using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.BLL.ViewModels.User
{
    public class ShowUserViewModel : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string? AboutUser { get; set; } = string.Empty;
    }
}
