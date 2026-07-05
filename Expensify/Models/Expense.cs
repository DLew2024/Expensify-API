using System.ComponentModel.DataAnnotations;

namespace Expensify.Models
{
    public class Expense : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; } = Guid.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Category{ get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime Date { get; set; } = new DateTime();
    }
}
