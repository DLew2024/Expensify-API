using System.ComponentModel.DataAnnotations;

namespace Expensify.Entities.Models
{
    public class Income 
    {
        [Required]
        public Guid UserId { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public int Amount {  get; set; }
        public DateTime Date { get; set; } 
    }
}
