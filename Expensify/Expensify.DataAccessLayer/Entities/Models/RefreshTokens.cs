using System;
using System.Collections.Generic;
using System.Text;
using Expensify.DataAccessLayer.Entities.AbstractClasses;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a refresh token issued to an authenticated user.
/// </summary>
public class RefreshToken : Identifiable
{
    /// <summary>
    /// The user who owns this refresh token.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Hash of the refresh token.
    /// The raw token must never be stored.
    /// </summary>
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>
    /// Date and time the refresh token expires.
    /// Stored as a Unix timestamp.
    /// </summary>
    public long ExpiresAt { get; set; }

    /// <summary>
    /// Date and time the token was revoked.
    /// Null while the token remains active.
    /// </summary>
    public long? RevokedAt { get; set; }

    /// <summary>
    /// Indicates whether the refresh token has been revoked.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Optional reason the token was revoked.
    /// Example: Logout, password change, or token rotation.
    /// </summary>
    public string? RevocationReason { get; set; }

    /// <summary>
    /// Navigation property for the token owner.
    /// </summary>
    public User User { get; set; } = null!;
}
