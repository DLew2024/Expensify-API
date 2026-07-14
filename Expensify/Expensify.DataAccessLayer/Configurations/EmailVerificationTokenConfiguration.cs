using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expensify.DataAccessLayer.Configurations;

/// <summary>
/// Configures database mappings for email-verification tokens.
/// </summary>
public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder
            .HasOne(token => token.User)
            .WithMany(user => user.EmailVerificationTokens)
            .HasForeignKey(token => token.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(token => token.TokenHash).IsUnique();

        builder.HasIndex(token => token.UserId);

        // Uses PostgreSQL's xmin system column to detect concurrent updates.
        builder.Property<uint>("xmin").IsRowVersion();
    }
}
