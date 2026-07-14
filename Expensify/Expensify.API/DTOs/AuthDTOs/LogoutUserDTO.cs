using Expensify.DataAccessLayer.Enums;

namespace Expensify.API.DTOs.AuthDTOs;

/// <summary>
/// Contains the refresh token for the session being logged out.
/// </summary>
public class LogoutUserDTO
{
    /// <summary>
    /// Determines whether to log out the current device or every device.
    /// </summary>
    public LogoutScope Scope { get; set; } = LogoutScope.CurrentDevice;

    /// <summary>
    /// The refresh token issued to the client during login or token refresh.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
