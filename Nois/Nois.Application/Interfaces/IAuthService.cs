using Nois.Application.DTOs;
using Nois.Application.DTOs.AuthDtos;
using Nois.Domain.Entities.Identity;

namespace Nois.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResultDto> RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto?> LoginAsync(LoginDto dto);
        Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string userId);
        Task SendEmailVerificationAsync(AppUser user, string origin);
        Task<bool> VerifyEmailAsync(string userId, string token);
    }
}
