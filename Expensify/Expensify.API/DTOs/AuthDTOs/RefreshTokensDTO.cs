namespace Expensify.API.DTOs.AuthDTOs;

public class RefreshTokensDTO
{
    /// <summary>
    /// The refresh token issued during login.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
