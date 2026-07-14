using Expensify.API.ServiceClasses.Interfaces;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages user profiles and account settings.
/// Responsibilities:
/// - Get current user
/// - Update profile
/// - Upload profile picture
/// - Update email
/// - Delete account
/// - User preferences
/// </summary>
public class UsersController : AuthorizationControllerBase
{
    private readonly IService _service;

    public UsersController(IService service)
    {
        _service = service;
    }
}
