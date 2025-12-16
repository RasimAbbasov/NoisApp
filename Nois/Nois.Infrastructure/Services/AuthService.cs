using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nois.Application.DTOs.AuthDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using Nois.Domain.Entities.Identity;
using Nois.Infrastructure.Options;
using System.Text;
  
namespace Nois.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly FrontendBaseUrlOptions _frontendOptions;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly JwtOptions _jwt;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IConfiguration config, IOptions<FrontendBaseUrlOptions> frontendOptions, ITokenService tokenService,IEmailService emailService,IOptions<JwtOptions> jwtOptions,IMapper mapper)
        {

            _userManager = userManager;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
            _signInManager = signInManager;
            _emailService = emailService;
            _tokenService = tokenService;
            _jwt = jwtOptions.Value;
        }

        // Check Register Method
        public async Task<RegisterResultDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new EmailAlreadyTaken();

            var user = _mapper.Map<AppUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
           
            if (!result.Succeeded)
                throw new RegisterFailedException(result.Errors);


            await _userManager.AddToRoleAsync(user, "User");

            var origin = _frontendOptions.BaseUrl;

            await SendEmailVerificationAsync(user, origin);

            return new RegisterResultDto { Success = true };
        }



        //CHECK OTHER METHODS //WRITE CHECKING CASES Handle if cases
        public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new UserNotFoundException();

            if (!user.EmailConfirmed) // WRITE CUSTOM EXCEPTION
                throw new EmailNotConfirmedException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);

            if (result.IsLockedOut)
              //User is locked out (too many failed attempts)
                throw new UserLockedOutException(); // Create this custom exception

            if (result.IsNotAllowed)
              //Login disabled (e.g., account requires MFA or is disabled by admin)
                throw new LoginDisabledException();

            if (!result.Succeeded)
                throw new InvalidCredentialsException();

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenService.GenerateAccessToken(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDays);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null ||
                !user.RefreshTokenExpiryTime.HasValue ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwt.RefreshTokenDays);

            await _userManager.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<bool> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task SendEmailVerificationAsync(AppUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = $"{origin}/VerifyEmail?userId={user.Id}&token={encodedToken}";

            await _emailService.SendEmailConfirmationAsync(user.Email, callbackUrl);
        }

        public async Task<bool> VerifyEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new UserNotFoundException();

            var tokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(tokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            return result.Succeeded;
        }

        public async Task<string?> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, string origin)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return null;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var resetLink = $"{origin}/ResetPassword?email={forgotPasswordDto.Email}&token={encodedToken}";

            await _emailService.SendPasswordResetEmailAsync(user.Email, resetLink);
            return resetLink;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return false;

            // Decode token from URL
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));

            var result = await _userManager.ResetPasswordAsync(user,decodedToken,dto.NewPassword);

            return result.Succeeded;
        }

    }
}
