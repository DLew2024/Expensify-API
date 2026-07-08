using Expensify.API.ServiceClasses.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages recurring transaction templates.
/// Responsibilities:
/// - Create recurring transactions
/// - Update recurring schedules
/// - Pause/resume recurring transactions
/// - Delete recurring transactions
/// - Generate scheduled transactions
/// </summary>
public class RecurringTransactionsController : AuthorizationController
{
    private readonly IService _service;

    public RecurringTransactionsController(IService service)
    {
        _service = service;
    }
}
