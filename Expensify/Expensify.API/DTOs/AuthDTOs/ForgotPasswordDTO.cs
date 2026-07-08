using System.ComponentModel.DataAnnotations;

namespace Expensify.API.DTOs.AuthDTOs;

public class ForgotPasswordDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
