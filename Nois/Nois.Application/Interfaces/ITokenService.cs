using Nois.Domain.Entities.Identity;

namespace Nois.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(AppUser user, IList<string> roles);
        string GenerateRefreshToken();
    }
}
