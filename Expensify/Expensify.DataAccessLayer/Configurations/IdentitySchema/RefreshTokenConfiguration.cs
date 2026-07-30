using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Expensify.DataAccessLayer.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.IdentitySchema;

/// <summary>
/// Configures the database mapping for the
/// <see cref="RefreshToken"/> entity.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures the <see cref="RefreshToken"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Maps the entity to the refresh_tokens table
        // in the identity schema.
        builder.ToTable("refresh_tokens", Schemas.Identity);

        // Configures the primary key.
        builder.HasKey(token => token.Id).HasName("pk_refresh_tokens");

        // Stores only the hashed refresh token.
        // The raw token should never be persisted.
        builder
            .Property(token => token.TokenHash)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.TokenHash);

        // Configures the required expiration timestamp.
        builder.Property(token => token.ExpiresAt).IsRequired();

        // Configures the optional revocation timestamp.
        builder.Property(token => token.RevokedAt).IsRequired(false);

        // Configures the revocation flag.
        builder.Property(token => token.IsRevoked).IsRequired();

        // Configures the optional revocation reason.
        builder
            .Property(token => token.RevocationReason)
            .HasMaxLength(DatabaseLengths.Description);

        // One user can have many refresh tokens.
        builder
            .HasOne(token => token.User)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(token => token.UserId)
            .HasConstraintName("fk_refresh_tokens_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Ensures each token hash is unique and supports direct token lookup.
        builder
            .HasIndex(token => token.TokenHash)
            .IsUnique()
            .HasDatabaseName("ux_refresh_tokens_token_hash");

        // Improves queries that locate a user's active, unexpired tokens.
        builder
            .HasIndex(token => new
            {
                token.UserId,
                token.IsRevoked,
                token.ExpiresAt,
            })
            .HasDatabaseName("ix_refresh_tokens_user_id_is_revoked_expires_at");

        // Uses PostgreSQL's xmin system column for optimistic concurrency.
        builder.Property<uint>("xmin").IsRowVersion();
    }
}
