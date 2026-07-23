namespace Expensify.API.ServiceClasses.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmail(
        string toEmail,
        string resetLink,
        CancellationToken cancellationToken
    );
}
