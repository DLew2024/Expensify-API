using System.ComponentModel.DataAnnotations;

namespace Expensify.API.DTOs.AuthDTOs
{
    public class UserResponseDTO
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
        public string ProfileImageURl { get; set; } = string.Empty;
    }
}
