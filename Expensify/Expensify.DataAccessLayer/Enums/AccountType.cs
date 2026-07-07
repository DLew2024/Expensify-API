namespace Expensify.DataAccessLayer.Enums;

/// <summary>
/// Represents the type of financial account.
/// </summary>
public enum AccountType
{
    /// <summary>
    /// A checking account used for everyday spending and deposits.
    /// Example: Chase Total Checking.
    /// </summary>
    Checking = 0,

    /// <summary>
    /// A savings account used to store money and earn interest.
    /// Example: High-yield savings account.
    /// </summary>
    Savings = 1,

    /// <summary>
    /// A credit card account used to make purchases on borrowed funds.
    /// Example: Discover It or Chase Freedom Unlimited.
    /// </summary>
    CreditCard = 2,

    /// <summary>
    /// Physical cash kept on hand.
    /// Example: Wallet or cash envelope.
    /// </summary>
    Cash = 3,

    /// <summary>
    /// An investment account used to hold stocks, ETFs, mutual funds, or other securities.
    /// Example: Fidelity Brokerage or Roth IRA.
    /// </summary>
    Investment = 4,

    /// <summary>
    /// A loan account with an outstanding balance.
    /// Example: Student loan, auto loan, or personal loan.
    /// </summary>
    Loan = 5,

    /// <summary>
    /// A retirement investment account.
    /// Example: 401(k), Traditional IRA, or Roth IRA.
    /// </summary>
    Retirement = 6,

    /// <summary>
    /// A mortgage account for a home loan.
    /// Example: Primary residence mortgage.
    /// </summary>
    Mortgage = 7,

    /// <summary>
    /// A line of credit that can be borrowed against as needed.
    /// Example: Home Equity Line of Credit (HELOC).
    /// </summary>
    LineOfCredit = 8,

    /// <summary>
    /// A digital payment account or wallet.
    /// Example: PayPal, Venmo, Apple Cash, or Cash App.
    /// </summary>
    DigitalWallet = 9,

    /// <summary>
    /// Any account type not represented by the predefined options.
    /// </summary>
    Other = 10,
}
