using Expensify.API.ServiceClasses.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages transaction categories.
/// Responsibilities:
/// - Create categories
/// - Update categories
/// - Delete categories
/// - Get user categories
/// - Manage default categories
/// </summary>
public class CategoriesController : AuthorizationControllerBase
{
    private readonly IService _service;

    public CategoriesController(IService service)
    {
        _service = service;
    }
}
