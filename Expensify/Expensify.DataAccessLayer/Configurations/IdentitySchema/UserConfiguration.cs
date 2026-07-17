using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.IdentitySchema;

/// <summary>
/// Configures the database mapping for the <see cref="User"/> entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the <see cref="User"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Maps the entity to the users table in the identity schema.
        builder.ToTable("users", "identity");

        // Configures the primary key.
        builder.HasKey(user => user.Id).HasName("pk_users");

        // Configures the user's required full name.
        builder
            .Property(user => user.FullName)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.FullName);

        // Configures the user's required email address.
        builder.Property(user => user.Email).IsRequired().HasMaxLength(DatabaseLengths.Email);

        // Configures the stored password hash.
        // Plain-text passwords should never be persisted.
        builder
            .Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.PasswordHash);

        // Configures the optional profile image URL.
        builder.Property(user => user.ProfileImageUrl).HasMaxLength(DatabaseLengths.Url);

        // Configures the email-verification status flag.
        builder.Property(user => user.IsEmailVerified).IsRequired();

        // Configures the optional email-verification timestamp.
        builder.Property(user => user.EmailVerifiedAt).IsRequired(false);

        // Ensures each email address is unique.
        builder.HasIndex(user => user.Email).IsUnique().HasDatabaseName("ux_users_email");
    }
}
