namespace Expensify.API.Services.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmail(
        string toEmail,
        string resetLink,
        CancellationToken cancellationToken
    );
}
