using Expensify.API.Services.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages members of shared budgets.
/// Responsibilities:
/// - Invite members
/// - Remove members
/// - Update member roles
/// - Accept invitations
/// - Decline invitations
/// - View budget members
/// - Transfer budget ownership
/// </summary>
public class BudgetMembersController : AuthorizationControllerBase
{
    private readonly IService _service;

    public BudgetMembersController(IService service)
    {
        _service = service;
    }
}
