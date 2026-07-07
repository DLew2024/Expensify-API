/// <summary>
/// Represents the type of financial goal.
/// </summary>
public enum GoalType
{
    /// <summary>
    /// Save money toward a general savings goal.
    /// Example: New laptop or home renovation.
    /// </summary>
    Savings = 0,

    /// <summary>
    /// Pay off debt over time.
    /// Example: Credit card, auto loan, or student loan.
    /// </summary>
    DebtPayoff = 1,

    /// <summary>
    /// Grow an investment portfolio.
    /// Example: Brokerage account or ETF portfolio.
    /// </summary>
    Investment = 2,

    /// <summary>
    /// Build an emergency fund.
    /// Example: Three to six months of living expenses.
    /// </summary>
    EmergencyFund = 3,

    /// <summary>
    /// Save for a vacation or trip.
    /// Example: Hawaii or Europe vacation.
    /// </summary>
    Vacation = 4,

    /// <summary>
    /// Save for a vehicle purchase.
    /// Example: Down payment on a new car.
    /// </summary>
    Vehicle = 5,

    /// <summary>
    /// Save for a home purchase.
    /// Example: Down payment or closing costs.
    /// </summary>
    Home = 6,

    /// <summary>
    /// Save for retirement.
    /// Example: Maxing out a Roth IRA or 401(k).
    /// </summary>
    Retirement = 7,

    /// <summary>
    /// Save for education expenses.
    /// Example: College tuition or certifications.
    /// </summary>
    Education = 8,

    /// <summary>
    /// Save for a major purchase.
    /// Example: Furniture, appliances, or electronics.
    /// </summary>
    MajorPurchase = 9,

    /// <summary>
    /// Save for a wedding or other major life event.
    /// </summary>
    LifeEvent = 10,

    /// <summary>
    /// Save for a business or side project.
    /// Example: Startup costs or business equipment.
    /// </summary>
    Business = 11,

    /// <summary>
    /// Save money for a child's future.
    /// Example: College fund or general savings.
    /// </summary>
    Child = 12,

    /// <summary>
    /// A user-defined financial goal.
    /// </summary>
    Custom = 13
}