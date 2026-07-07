namespace Expensify.DataAccessLayer.Enums;

/// <summary>
/// Represents the type of financial transaction.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Money leaving an account.
    /// Example: Paying rent or buying groceries.
    /// </summary>
    Expense = 0,

    /// <summary>
    /// Money entering an account.
    /// Example: Receiving a paycheck or tax refund.
    /// </summary>
    Income = 1,

    /// <summary>
    /// Money moved between two accounts owned by the user.
    /// Example: Transferring funds from checking to savings.
    /// </summary>
    Transfer = 2,
}
