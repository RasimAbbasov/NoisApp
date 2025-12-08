using Microsoft.AspNetCore.Identity;

namespace Nois.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string? ProfileImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
