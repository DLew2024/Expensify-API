using System.ComponentModel.DataAnnotations;

namespace Expensify.API.DTOs.AuthDTOs
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
