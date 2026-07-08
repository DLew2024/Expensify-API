namespace Expensify.API.DTOs.AuthDTOs;

public class ResetPasswordDTO
{
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
