namespace Expensify.API.DTOs.AuthDTOs;

public class ChangePasswordDTO
{
    /// <summary>
    /// The user's current password.
    /// </summary>
    public string CurrentPassword { get; set; } = string.Empty;

    /// <summary>
    /// The new password the user wants to use.
    /// </summary>
    public string NewPassword { get; set; } = string.Empty;

    /// <summary>
    /// Confirmation of the new password.
    /// </summary>
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
