using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expensify.DataAccessLayer.Configurations;

/// <summary>
/// Configures the database mapping for the <see cref="RefreshToken"/> entity.
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
        builder.ToTable("refresh_tokens");

        builder.HasKey(x => x.Id);

        /// <summary>
        /// Configures the relationship between a refresh token and its owning user.
        /// A user can have many refresh tokens, while each refresh token belongs
        /// to exactly one user.
        /// </summary>
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        /// <summary>
        /// Creates an index on the token hash to speed up refresh token lookups.
        /// </summary>
        builder.HasIndex(x => x.TokenHash).IsUnique();

        /// <summary>
        /// Creates an index on the user identifier to optimize queries that
        /// retrieve or revoke a user's refresh tokens.
        /// </summary>
        builder.HasIndex(x => x.UserId);

        /// <summary>
        /// Uses PostgreSQL's built-in xmin system column as an optimistic
        /// concurrency token. EF Core will throw a
        /// <see cref="DbUpdateConcurrencyException"/> if another transaction
        /// modifies this row before the current transaction is committed.
        /// </summary>
        builder.Property<uint>("xmin").IsRowVersion();
    }
}
