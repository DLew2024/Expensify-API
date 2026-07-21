using Expensify.DataAccessLayer.Entities.Models.IdentitySchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.IdentitySchema;

/// <summary>
/// Configures the database mapping for the <see cref="Role"/> entity.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Configures the <see cref="Role"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Maps roles to the identity schema.
        builder.ToTable("roles", "identity");

        // Configures the primary key.
        builder.HasKey(role => role.Id).HasName("pk_roles");

        // Configures required properties.
        builder.Property(role => role.Name).IsRequired().HasMaxLength(DatabaseLengths.RoleName);

        // Ensures role names are unique.
        builder.HasIndex(role => role.Name).IsUnique().HasDatabaseName("ux_roles_name");

        // Configures the relationship between roles and users.
        builder
            .HasMany(role => role.Users)
            .WithOne(user => user.Role)
            .HasForeignKey(user => user.RoleId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_users_roles_role_id");

        builder.HasData(
            new Role
            {
                Id = RoleIds.User,
                Name = RoleNames.User,
            },
            new Role
            {
                Id = RoleIds.Admin,
                Name = RoleNames.Admin,
            }
        );
    }
}
