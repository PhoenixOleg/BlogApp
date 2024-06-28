using Microsoft.AspNetCore.Identity;

namespace BlogApp.StaticServices
{
    public static class DefaultUser
    {
        public static readonly IdentityUser Administrator = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@your_email_domain.zone",
            EmailConfirmed = true
        };
    }
}
