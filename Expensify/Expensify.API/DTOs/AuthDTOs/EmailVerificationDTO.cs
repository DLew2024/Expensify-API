namespace Expensify.API.DTOs.AuthDTOs;

public class EmailVerificationDTO
{
    /// <summary>
    /// The raw email-verification token sent to the user's email.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
