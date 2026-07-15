namespace Expensify.API.Configurations;

/// <summary>
/// Represents the JWT authentication settings used to issue and validate
/// access tokens and refresh tokens.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// The secret key used to sign JWT access tokens.
    /// This value should be stored securely using User Secrets during development
    /// and a secure secret store or environment variable in production.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// The issuer of the JWT.
    /// Typically the name or URL of the authentication server.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The intended audience for the JWT.
    /// Typically the client application that will consume the token.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// The lifetime of an access token, in minutes.
    /// Access tokens should remain short-lived for security purposes.
    /// </summary>
    public int ExpirationMinutes { get; set; } = 15;

    /// <summary>
    /// The lifetime of a refresh token, in days.
    /// Refresh tokens are long-lived credentials used to obtain new access tokens.
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 30;
}
