using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public abstract class AuthorizationControllerBase : ControllerBase
{
    /// <summary>
    /// Gets the unique identifier of the currently authenticated user.
    /// </summary>
    protected Guid CurrentUserId
    {
        get
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException(
                    "The authenticated user's identifier is missing or invalid."
                );
            }

            return userId;
        }
    }
}
