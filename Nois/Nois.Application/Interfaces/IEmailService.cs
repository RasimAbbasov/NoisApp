namespace Nois.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string callbackUrl);
        Task SendPasswordResetEmailAsync(string toEmail, string callbackUrl);
    }
}
