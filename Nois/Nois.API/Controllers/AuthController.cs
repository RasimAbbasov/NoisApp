using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.AuthDtos;
using Nois.Application.Exceptions;
using Nois.Application.Interfaces;
using System.Security.Claims;

namespace Nois.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var errors = await _authService.RegisterAsync(dto);
            if (errors.Errors.Any())
                return BadRequest(errors);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var tokens = await _authService.LoginAsync(dto);
            if (tokens == null)
                return Unauthorized("Invalid credentials");

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
        {
            var tokens = await _authService.RefreshTokenAsync(dto.RefreshToken);
            if (tokens == null)
                return Unauthorized("Invalid or expired refresh token");

            return Ok(tokens);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            await _authService.LogoutAsync(userId);
            return Ok("Logged out");
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var success = await _authService.VerifyEmailAsync(userId, token);
            if (!success)
                return BadRequest("Email verification failed");

            return Ok("Email successfully verified");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var origin = Request.Headers["origin"].ToString();

            await _authService.ForgotPasswordAsync(dto, origin);

            return Ok("If an account with that email exists, a reset link has been sent.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);

            if (!result)
                return BadRequest("Password reset failed");

            return Ok("Password has been reset successfully");
        }

    }
}
