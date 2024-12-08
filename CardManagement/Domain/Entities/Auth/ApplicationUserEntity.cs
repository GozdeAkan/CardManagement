
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Auth
{
    public class ApplicationUserEntity: IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
    }
}
