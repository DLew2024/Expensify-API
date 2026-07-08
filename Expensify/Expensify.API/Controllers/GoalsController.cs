using Expensify.API.ServiceClasses.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages user financial goals.
/// Responsibilities:
/// - Create goals
/// - Update goals
/// - Delete goals
/// - Track progress
/// - Mark goals complete
/// - View goal statistics
/// </summary>
public class GoalsController : AuthorizationController
{
    private readonly IService _service;

    public GoalsController(IService service)
    {
        _service = service;
    }
}
