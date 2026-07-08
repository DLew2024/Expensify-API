using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expensify.DataAccessLayer.Configurations;

/// <summary>
/// Configures the database mapping for the <see cref="PasswordResetToken"/> entity.
/// Defines relationships, indexes, constraints, and other Entity Framework Core
/// configuration that should not live inside the entity itself.
/// </summary>
public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    /// <summary>
    /// Configures the <see cref="PasswordResetToken"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        // A password reset token belongs to exactly one user.
        // A user can have many password reset tokens.
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.PasswordResetTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Improves lookup performance when searching for a token by its hash.
        builder.HasIndex(x => x.TokenHash);

        // Improves lookup performance when retrieving all tokens for a user.
        builder.HasIndex(x => x.UserId);
    }
}
