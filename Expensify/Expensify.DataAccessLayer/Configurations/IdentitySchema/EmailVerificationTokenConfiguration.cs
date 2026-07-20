using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.IdentitySchema;

/// <summary>
/// Configures the database mapping for the
/// <see cref="EmailVerificationToken"/> entity.
/// </summary>
public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    /// <summary>
    /// Configures the <see cref="EmailVerificationToken"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        // Maps the entity to the email_verification_tokens table
        // in the identity schema.
        builder.ToTable("email_verification_tokens", "identity");

        // Configures the primary key.
        builder.HasKey(token => token.Id).HasName("pk_email_verification_tokens");

        // Stores only the hashed verification token.
        builder
            .Property(token => token.TokenHash)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.TokenHash);

        // Configures the token expiration timestamp.
        builder.Property(token => token.ExpiresAt).IsRequired();

        // Configures the optional timestamp indicating when the token was used.
        builder.Property(token => token.UsedAt).IsRequired(false);

        // Configures the revocation flag.
        builder.Property(token => token.IsRevoked).IsRequired();

        // One user can have many email-verification tokens.
        builder
            .HasOne(token => token.User)
            .WithMany(user => user.EmailVerificationTokens)
            .HasForeignKey(token => token.UserId)
            .HasConstraintName("fk_email_verification_tokens_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Ensures each token hash is unique and supports direct token lookup.
        builder
            .HasIndex(token => token.TokenHash)
            .IsUnique()
            .HasDatabaseName("ux_email_verification_tokens_token_hash");

        // Improves queries that locate a user's active, unexpired tokens.
        builder
            .HasIndex(token => new
            {
                token.UserId,
                token.IsRevoked,
                token.ExpiresAt,
            })
            .HasDatabaseName("ix_email_verification_tokens_user_id_is_revoked_expires_at");

        // Uses PostgreSQL's xmin system column for optimistic concurrency.
        builder.Property<uint>("xmin").IsRowVersion();
    }
}
