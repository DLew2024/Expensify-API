namespace Expensify.DataAccessLayer.Enums;

public enum LogoutScope
{
    /// <summary>
    /// Logs out only the current device by revoking the supplied refresh token.
    /// </summary>
    CurrentDevice = 0,

    /// <summary>
    /// Logs out all devices by revoking every active refresh token for the user.
    /// </summary>
    AllDevices = 1,
}
