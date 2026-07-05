namespace Expensify.DTOs.IncomeDTOs
{
    public class AddIncomeDTO
    {
        public Guid UserID { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
    }
}
