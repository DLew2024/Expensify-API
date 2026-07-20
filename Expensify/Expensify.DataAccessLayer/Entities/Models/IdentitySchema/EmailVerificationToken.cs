using Expensify.DataAccessLayer.Entities.AbstractClasses;

namespace Expensify.DataAccessLayer.Entities.Models.IdentitySchema;

/// <summary>
/// Represents a token issued to verify a user's email address.
/// </summary>
public class EmailVerificationToken : Identifiable
{
    /// <summary>
    /// The user associated with this verification token.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Hash of the verification token.
    /// The raw token must not be stored.
    /// </summary>
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>
    /// Date the token expires.
    /// Stored as a Unix timestamp.
    /// </summary>
    public long ExpiresAt { get; set; }

    /// <summary>
    /// Date the token was consumed.
    /// Null if it has not been used.
    /// </summary>
    public long? UsedAt { get; set; }

    /// <summary>
    /// Indicates whether the token was manually invalidated.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Navigation property for the associated user.
    /// </summary>
    public User User { get; set; } = null!;
}
