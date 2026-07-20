using Expensify.DataAccessLayer.Entities.AbstractClasses;

namespace Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

/// <summary>
/// Represents a password reset token issued to a user.
/// The token is used to securely verify password reset requests.
/// </summary>
public class PasswordResetToken : Identifiable
{
    /// <summary>
    /// The user who owns this password reset token.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The hashed value of the password reset token.
    /// The raw token should never be stored in the database.
    /// </summary>
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the token expires.
    /// Stored as a Unix timestamp.
    /// </summary>
    public long ExpiresAt { get; set; }

    /// <summary>
    /// The date and time when the token was used.
    /// Null if the token has not yet been used.
    /// Stored as a Unix timestamp.
    /// </summary>
    public long? UsedAt { get; set; }

    /// <summary>
    /// Indicates whether the token has been revoked before its expiration.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Navigation property for the user who owns this password reset token.
    /// </summary>
    public User User { get; set; } = null!;
}
