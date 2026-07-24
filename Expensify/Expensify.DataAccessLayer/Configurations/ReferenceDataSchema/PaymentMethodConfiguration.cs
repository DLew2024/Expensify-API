using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.ReferenceDataSchema;

/// <summary>
/// Configures the database mapping for the <see cref="PaymentMethod"/> entity.
/// </summary>
public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    /// <summary>
    /// Configures the <see cref="PaymentMethod"/> entity.
    /// </summary>
    /// <param name="builder">
    /// The builder used to configure the entity's database mapping.
    /// </param>
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        // Maps the entity to the payment_methods table in the reference_data schema.
        builder.ToTable("payment_methods", "reference_data");

        // Configures the primary key.
        builder.HasKey(paymentMethod => paymentMethod.Id).HasName("pk_payment_methods");

        // Configures the required payment-method name.
        builder
            .Property(paymentMethod => paymentMethod.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures the optional description.
        builder
            .Property(paymentMethod => paymentMethod.Description)
            .HasMaxLength(DatabaseLengths.Description);

        // One user can create many payment methods.
        builder
            .HasOne(paymentMethod => paymentMethod.User)
            .WithMany(user => user.PaymentMethods)
            .HasForeignKey(paymentMethod => paymentMethod.UserId)
            .HasConstraintName("fk_payment_methods_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One payment method can be used by many transactions.
        builder
            .HasMany(paymentMethod => paymentMethod.Transactions)
            .WithOne(transaction => transaction.PaymentMethod)
            .HasForeignKey(transaction => transaction.PaymentMethodId)
            .HasConstraintName("fk_transactions_payment_methods_payment_method_id")
            .OnDelete(DeleteBehavior.SetNull);

        // Prevents a user from creating duplicate payment-method names.
        builder
            .HasIndex(paymentMethod => new { paymentMethod.UserId, paymentMethod.Name })
            .IsUnique()
            .HasDatabaseName("ux_payment_methods_user_id_name");

        // Improves queries that retrieve active payment methods for a user.
        builder
            .HasIndex(paymentMethod => new { paymentMethod.UserId, paymentMethod.IsActive })
            .HasDatabaseName("ix_payment_methods_user_id_is_active");

        // Seeds system-default payment methods.
        builder.HasData(
            new
            {
                Id = Guid.Parse("30a94670-364a-4515-aedc-04c4e59e70ac"),
                Name = "Cash",
                UserId = (Guid?)null,
                Description = "Payment made using physical cash.",
                IsSystemDefault = true,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0L,
                UpdatedDate = 0L,
            },
            new
            {
                Id = Guid.Parse("64f59d3d-810f-4814-a951-5daeb7866237"),
                Name = "Debit Card",
                UserId = (Guid?)null,
                Description = "Payment made using a debit card.",
                IsSystemDefault = true,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0L,
                UpdatedDate = 0L,
            },
            new
            {
                Id = Guid.Parse("e28f1e04-06fd-45cc-8749-25427024c8db"),
                Name = "Credit Card",
                UserId = (Guid?)null,
                Description = "Payment made using a credit card.",
                IsSystemDefault = true,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0L,
                UpdatedDate = 0L,
            },
            new
            {
                Id = Guid.Parse("984931ed-5f87-43d9-a9df-5439b528f321"),
                Name = "Bank Transfer",
                UserId = (Guid?)null,
                Description = "Payment made through a bank transfer.",
                IsSystemDefault = true,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0L,
                UpdatedDate = 0L,
            },
            new
            {
                Id = Guid.Parse("c620d7dc-20be-4306-a76f-b33b62ee35b4"),
                Name = "Check",
                UserId = (Guid?)null,
                Description = "Payment made using a check.",
                IsSystemDefault = true,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0L,
                UpdatedDate = 0L,
            },
            new
            {
                Id = Guid.Parse("9017dba1-62df-4661-8d33-de7f37df8a7a"),
                Name = "Digital Wallet",
                UserId = (Guid?)null,
                Description = "Payment made using a digital wallet.",
                IsSystemDefault = true,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0L,
                UpdatedDate = 0L,
            }
        );
    }
}
