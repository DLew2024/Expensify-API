namespace Expensify.API.DTOs.AuthDTOs;

public class RefreshTokenResponseDTO
{
    /// <summary>
    /// Newly issued JWT access token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Newly issued refresh token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Number of seconds until the access token expires.
    /// </summary>
    public int ExpiresIn { get; set; }
}
