namespace Expensify.DataAccessLayer.Enums;

public enum TransactionStatus
{
    /// <summary>
    /// The transaction has been initiated but has not yet been completed.
    /// Example: A debit card purchase awaiting bank processing.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The transaction has been successfully processed and reflected in the account balance.
    /// Example: A completed paycheck deposit or posted credit card purchase.
    /// </summary>
    Posted = 1,

    /// <summary>
    /// The transaction was voided or will not be processed.
    /// Example: A cancelled payment or reversed authorization.
    /// </summary>
    Cancelled = 2,
}
