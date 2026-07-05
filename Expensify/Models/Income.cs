using System.ComponentModel.DataAnnotations;

namespace Expensify.Models
{
    public class Income : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public int Amount {  get; set; }
        public DateTime Date { get; set; } 
    }
}
