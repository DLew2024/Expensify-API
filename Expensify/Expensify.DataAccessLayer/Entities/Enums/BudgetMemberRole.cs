using System;
using System.Collections.Generic;
using System.Text;

namespace Expensify.DataAccessLayer.Entities.Enums
{
    /// <summary>
    /// Defines a user's permission level for a shared budget.
    /// </summary>
    public enum BudgetMemberRole
    {
        /// <summary>
        /// Has full ownership of the budget.
        /// Can edit all budget details, manage members, transfer ownership, and delete the budget.
        /// </summary>
        Owner = 0,

        /// <summary>
        /// Has administrative access to the budget.
        /// Can edit budget details and manage members, but cannot transfer ownership or delete the budget.
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Can create, edit, and remove transactions, categories, and budget limits.
        /// Cannot manage members, transfer ownership, or delete the budget.
        /// </summary>
        Editor = 2,

        /// <summary>
        /// Has read-only access to the budget.
        /// Can view budget details and transactions but cannot make any changes.
        /// </summary>
        Viewer = 3,
    }
}
