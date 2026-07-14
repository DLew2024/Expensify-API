using Expensify.API.ServiceClasses.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages financial accounts.
/// Responsibilities:
/// - Create accounts
/// - Update accounts
/// - Delete/close accounts
/// - Get account details
/// - Get account balances
/// - Hide/unhide accounts
/// - Calculate net worth
/// </summary>
public class AccountsController : AuthorizationControllerBase
{
    private readonly IService _service;

    public AccountsController(IService service)
    {
        _service = service;
    }
}
