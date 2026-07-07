namespace Expensify.DataAccessLayer.Enums;

public enum RecurringFrequency
{
    /// <summary>
    /// Occurs every day.
    /// Example: Daily allowance.
    /// </summary>
    Daily = 0,

    /// <summary>
    /// Occurs once every week.
    /// Example: Weekly paycheck.
    /// </summary>
    Weekly = 1,

    /// <summary>
    /// Occurs every two weeks.
    /// Example: Bi-weekly paycheck every other Friday.
    /// </summary>
    BiWeekly = 2,

    /// <summary>
    /// Occurs twice per month.
    /// Example: Mortgage payment on the 1st and 15th.
    /// </summary>
    SemiMonthly = 3,

    /// <summary>
    /// Occurs once every month.
    /// Example: Rent or Netflix subscription.
    /// </summary>
    Monthly = 4,

    /// <summary>
    /// Occurs once every two months.
    /// Example: Utility bill billed every other month.
    /// </summary>
    EveryTwoMonths = 5,

    /// <summary>
    /// Occurs once every three months.
    /// Example: Quarterly estimated tax payment.
    /// </summary>
    Quarterly = 6,

    /// <summary>
    /// Occurs once every four months.
    /// Example: Scheduled maintenance or recurring service.
    /// </summary>
    EveryFourMonths = 7,

    /// <summary>
    /// Occurs once every six months.
    /// Example: Auto insurance premium.
    /// </summary>
    SemiAnnually = 8,

    /// <summary>
    /// Occurs once every year.
    /// Example: Amazon Prime membership or property taxes.
    /// </summary>
    Yearly = 9,
}
