using System.ComponentModel.DataAnnotations;

namespace Expensify.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string ProfileimageURl { get; set; } = string.Empty;
        
        // Hash password before storing  
        // Compare passwords
    }
}
