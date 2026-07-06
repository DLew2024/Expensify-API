using System.ComponentModel.DataAnnotations;

namespace Expensify.Models
{
    public class User 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Pid { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string ProfileimageURl { get; set; } = string.Empty;
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();

        // Hash password before storing  
        // Compare passwords
    }
}
