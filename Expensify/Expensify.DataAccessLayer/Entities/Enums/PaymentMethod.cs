namespace Expensify.DataAccessLayer.Entities.Enums
{
    /// <summary>
    /// Represents the method used to complete a financial transaction.
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Payment made using physical cash.
        /// Example: Paying for lunch with cash.
        /// </summary>
        Cash = 0,

        /// <summary>
        /// Payment made using a debit card.
        /// Example: Grocery purchase using a checking account.
        /// </summary>
        DebitCard = 1,

        /// <summary>
        /// Payment made using a credit card.
        /// Example: Online purchase with a Visa credit card.
        /// </summary>
        CreditCard = 2,

        /// <summary>
        /// Funds transferred directly between bank accounts.
        /// Example: ACH payment or wire transfer.
        /// </summary>
        BankTransfer = 3,

        /// <summary>
        /// Payment made by paper check.
        /// Example: Mailing a rent check.
        /// </summary>
        Check = 4,

        /// <summary>
        /// Payment made using a digital wallet or mobile payment service.
        /// Example: Apple Pay or Google Pay.
        /// </summary>
        MobilePayment = 5,

        /// <summary>
        /// Any payment method not represented by the predefined options.
        /// </summary>
        Other = 6,
    }
}
