using Expensify.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.FinanceSchema;

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
        // Maps the entity to the payment_methods table in the finance schema.
        builder.ToTable("payment_methods", "finance");

        // Configures the primary key.
        builder.HasKey(paymentMethod => paymentMethod.Id)
            .HasName("pk_payment_methods");

        // Configures the required payment-method name.
        builder.Property(paymentMethod => paymentMethod.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures the optional description.
        builder.Property(paymentMethod => paymentMethod.Description)
            .HasMaxLength(DatabaseLengths.Description);

        // One user can create many payment methods.
        builder.HasOne(paymentMethod => paymentMethod.User)
            .WithMany(user => user.PaymentMethods)
            .HasForeignKey(paymentMethod => paymentMethod.UserId)
            .HasConstraintName("fk_payment_methods_users_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        // One payment method can be used by many transactions.
        builder.HasMany(paymentMethod => paymentMethod.Transactions)
            .WithOne(transaction => transaction.PaymentMethod)
            .HasForeignKey(transaction => transaction.PaymentMethodId)
            .HasConstraintName(
                "fk_transactions_payment_methods_payment_method_id"
            )
            .OnDelete(DeleteBehavior.SetNull);

        // Prevents a user from creating duplicate payment-method names.
        builder.HasIndex(paymentMethod => new
        {
            paymentMethod.UserId,
            paymentMethod.Name
        })
        .IsUnique()
        .HasDatabaseName("ux_payment_methods_user_id_name");

        // Improves queries that retrieve active payment methods for a user.
        builder.HasIndex(paymentMethod => new
        {
            paymentMethod.UserId,
            paymentMethod.IsActive
        })
        .HasDatabaseName("ix_payment_methods_user_id_is_active");
    }
}