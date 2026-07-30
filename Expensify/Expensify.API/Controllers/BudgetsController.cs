using Expensify.API.Services.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages budgets.
/// Responsibilities:
/// - Create budgets
/// - Update budgets
/// - Delete budgets
/// - Share budgets
/// - Assign categories
/// - Track spending
/// - Calculate remaining budget
/// - View budget progress
/// </summary>
public class BudgetsController : AuthorizationControllerBase
{
    private readonly IService _service;

    public BudgetsController(IService service)
    {
        _service = service;
    }
}
