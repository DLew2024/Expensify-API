namespace Expensify.API.DTOs.AuthDTOs;

public class RefreshTokensDTO
{
    /// <summary>
    /// The expired access token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// The refresh token issued during login.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
