namespace Nois.Application.DTOs.AuthDtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string UserName { get; set; } // optional
        public string Password { get; set; }
    }
}
