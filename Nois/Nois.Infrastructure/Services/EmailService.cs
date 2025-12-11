using Nois.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Nois.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailConfirmationAsync(string toEmail, string callbackUrl)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Nois App", _config["EmailSettings:SenderEmail"]));
            email.To.Add(new MailboxAddress(toEmail, toEmail));
            email.Subject = "Confirm your email";

            var body = $@"
                <h2>Welcome to Nois App!</h2>
                <p>Please verify your email by clicking the link below:</p>
                <p><a href='{callbackUrl}' style='padding:10px 20px;
                    background-color:#4CAF50;color:white;text-decoration:none;
                    border-radius:5px;'>Verify Email</a></p>
                <br/>
                <p>If you did not create an account, ignore this email.</p>
            ";

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendPasswordResetEmailAsync(string toEmail, string callbackUrl)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Nois App", _config["EmailSettings:SenderEmail"]));
            email.To.Add(new MailboxAddress(toEmail, toEmail));
            email.Subject = "Reset your password";

            var body = $@"
                <h2>Welcome to Nois App!</h2>
                <p>Please reset your password by clicking the link below:</p>
                <p><a href='{callbackUrl}' style='padding:10px 20px;
                    background-color:#4CAF50;color:white;text-decoration:none;
                    border-radius:5px;'>Verify Email</a></p>
                <br/>
            ";

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
