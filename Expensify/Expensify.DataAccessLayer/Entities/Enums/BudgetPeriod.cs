using System;
using System.Collections.Generic;
using System.Text;

namespace Expensify.DataAccessLayer.Entities.Enums
{
    /// <summary>
    /// Defines how often a budget resets or is evaluated.
    /// </summary>
    public enum BudgetPeriod
    {
        /// <summary>
        /// Resets every day.
        /// Example: $15 daily coffee budget.
        /// </summary>
        Daily = 0,

        /// <summary>
        /// Resets once every week.
        /// Example: Weekly grocery budget.
        /// </summary>
        Weekly = 1,

        /// <summary>
        /// Resets every two weeks.
        /// Example: Budget aligned with a bi-weekly paycheck.
        /// </summary>
        BiWeekly = 2,

        /// <summary>
        /// Resets twice per month.
        /// Example: Budget from the 1st–15th and 16th–end of the month.
        /// </summary>
        SemiMonthly = 3,

        /// <summary>
        /// Resets once every month.
        /// Example: Monthly dining or entertainment budget.
        /// </summary>
        Monthly = 4,

        /// <summary>
        /// Resets once every three months.
        /// Example: Quarterly business expense budget.
        /// </summary>
        Quarterly = 5,

        /// <summary>
        /// Resets once every six months.
        /// Example: Semi-annual insurance budget.
        /// </summary>
        SemiAnnually = 6,

        /// <summary>
        /// Resets once every year.
        /// Example: Annual vacation budget.
        /// </summary>
        Yearly = 7,

        /// <summary>
        /// Applies to a single occurrence and does not automatically reset.
        /// Example: New laptop purchase budget.
        /// </summary>
        OneTime = 8,

        /// <summary>
        /// Uses a user-defined budgeting period.
        /// Example: A budget with custom start and end dates.
        /// </summary>
        Custom = 9,
    }
}
