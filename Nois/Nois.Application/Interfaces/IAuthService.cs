using Nois.Application.DTOs;
using Nois.Application.DTOs.AuthDtos;

namespace Nois.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string[]> RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto?> LoginAsync(LoginDto dto);
        Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string userId);
    }
}
