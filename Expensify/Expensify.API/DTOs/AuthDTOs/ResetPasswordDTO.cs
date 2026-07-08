using System.ComponentModel.DataAnnotations;

namespace Expensify.API.DTOs.AuthDTOs;

public class ResetPasswordDTO
{
    [Required]
    public string Token { get; set; } = string.Empty;
    
    [Required]
    public string NewPassword { get; set; } = string.Empty;

}
