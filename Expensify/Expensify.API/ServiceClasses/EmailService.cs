using System.Net;
using System.Net.Mail;
using Expensify.API.Configurations;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.Extensions.Options;

namespace Expensify.API.ServiceClasses
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendPasswordResetEmail(
            string toEmail,
            string resetLink,
            CancellationToken cancellationToken
        )
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = "Reset your password",
                Body = $"""
                    <p>You requested to reset your password.</p>
                    <p>Click the link below to reset it:</p>
                    <p><a href="{resetLink}">Reset Password</a></p>
                    <p>If you did not request this, you can ignore this email.</p>
                    """,
                IsBodyHtml = true,
            };

            message.To.Add(toEmail);

            using var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(
                    _emailSettings.SmtpUsername,
                    _emailSettings.SmtpPassword
                ),
                EnableSsl = _emailSettings.EnableSsl,
            };

            await smtpClient.SendMailAsync(message, cancellationToken);
        }
    }
}
