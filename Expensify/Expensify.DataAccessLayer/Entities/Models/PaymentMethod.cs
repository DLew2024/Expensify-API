using System;
using System.Collections.Generic;
using System.Text;
using Expensify.DataAccessLayer.Entities.AbstractClasses;

namespace Expensify.DataAccessLayer.Entities.Models;

/// <summary>
/// Represents a payment method that can be associated with transactions.
/// Payment methods may be system-defined or created by the user.
/// </summary>
public class PaymentMethod : Auditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentMethod"/> class.
    /// </summary>
    protected PaymentMethod() { }

    /// <summary>
    /// Initializes a new payment method with the specified name.
    /// </summary>
    /// <param name="name">The display name of the payment method.</param>
    public PaymentMethod(string name)
        : base(name) { }

    /// <summary>
    /// The user who owns this payment method.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Optional description of the payment method.
    /// Example: "Primary Visa card" or "Cash in wallet".
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether this payment method was provided by the system.
    /// System payment methods cannot typically be deleted.
    /// </summary>
    public bool IsSystemDefault { get; set; }

    /// <summary>
    /// Indicates whether this payment method is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property for the owner of the payment method.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Transactions that use this payment method.
    /// </summary>
    public List<Transaction> Transactions { get; set; } = [];
}
