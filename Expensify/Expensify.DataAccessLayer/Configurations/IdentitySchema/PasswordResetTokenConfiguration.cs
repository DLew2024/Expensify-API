using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.IdentitySchema;

/// <summary>
/// Configures the database mapping for the
/// <see cref="PasswordResetToken"/> entity.
/// </summary>
public class PasswordResetTokenConfiguration
    : IEntityTypeConfiguration<PasswordResetToken>
{
    /// <summary>
    /// Configures the <see cref="PasswordResetToken"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        // Maps the entity to the password_reset_tokens table
        // in the identity schema.
        builder.ToTable("password_reset_tokens", "identity");

        // Configures the primary key.
        builder.HasKey(token => token.Id)
            .HasName("pk_password_reset_tokens");

        // Stores only the hashed password-reset token.
        // The raw token should never be persisted.
        builder.Property(token => token.TokenHash)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.TokenHash);

        // Configures the required expiration timestamp.
        builder.Property(token => token.ExpiresAt)
            .IsRequired();

        // Configures the optional timestamp indicating when the token was used.
        builder.Property(token => token.UsedAt)
            .IsRequired(false);

        // Configures the token revocation flag.
        builder.Property(token => token.IsRevoked)
            .IsRequired();

        // One user can have many password-reset tokens.
        builder.HasOne(token => token.User)
            .WithMany(user => user.PasswordResetTokens)
            .HasForeignKey(token => token.UserId)
            .HasConstraintName(
                "fk_password_reset_tokens_users_user_id"
            )
            .OnDelete(DeleteBehavior.Cascade);

        // Ensures each token hash is unique and supports direct token lookup.
        builder.HasIndex(token => token.TokenHash)
            .IsUnique()
            .HasDatabaseName(
                "ux_password_reset_tokens_token_hash"
            );

        // Improves queries that locate a user's active, unexpired tokens.
        builder.HasIndex(token => new
        {
            token.UserId,
            token.IsRevoked,
            token.ExpiresAt
        })
        .HasDatabaseName(
            "ix_password_reset_tokens_user_id_is_revoked_expires_at"
        );

        // Uses PostgreSQL's xmin system column for optimistic concurrency.
        builder.Property<uint>("xmin")
            .IsRowVersion();
    }
}