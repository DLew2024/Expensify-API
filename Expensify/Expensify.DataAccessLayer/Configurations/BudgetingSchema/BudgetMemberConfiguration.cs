using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Expensify.DataAccessLayer.Configurations.BudgetingSchema;

/// <summary>
/// Configures the database mapping for the <see cref="BudgetMember"/> entity.
/// </summary>
public class BudgetMemberConfiguration : IEntityTypeConfiguration<BudgetMember>
{
    /// <summary>
    /// Configures the <see cref="BudgetMember"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<BudgetMember> builder)
    {
        // Maps the entity to the budget_members table in the budgeting schema.
        builder.ToTable("budget_members", "budgeting");

        // Configures the primary key.
        builder.HasKey(member => member.Id).HasName("pk_budget_members");

        // Configures the member's budget role.
        builder.Property(member => member.Role).IsRequired();

        // Configures the timestamp for when the member joined.
        builder.Property(member => member.JoinedDate).IsRequired();

        // Configures the active membership flag.
        builder.Property(member => member.IsActive).IsRequired();

        // One budget can have many members.
        builder
            .HasOne(member => member.Budget)
            .WithMany(budget => budget.Members)
            .HasForeignKey(member => member.BudgetId)
            .HasConstraintName("fk_budget_members_budgets_budget_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One user can belong to many shared budgets.
        builder
            .HasOne(member => member.User)
            .WithMany(user => user.BudgetMemberships)
            .HasForeignKey(member => member.UserId)
            .HasConstraintName("fk_budget_members_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // Prevents duplicate membership records for the same user and budget.
        builder
            .HasIndex(member => new { member.BudgetId, member.UserId })
            .IsUnique()
            .HasDatabaseName("ux_budget_members_budget_id_user_id");

        // Improves queries that load all shared budgets for a user.
        builder.HasIndex(member => member.UserId).HasDatabaseName("ix_budget_members_user_id");

        // Improves queries that retrieve active members for a budget.
        builder
            .HasIndex(member => new { member.BudgetId, member.IsActive })
            .HasDatabaseName("ix_budget_members_budget_id_is_active");
    }
}
