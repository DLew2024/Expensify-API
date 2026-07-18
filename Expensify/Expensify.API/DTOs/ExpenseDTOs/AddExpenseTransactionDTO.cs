using System.ComponentModel.DataAnnotations;

namespace Expensify.API.DTOs.ExpenseDTOs;

public class AddExpenseTransactionDTO
{
    [Required]
    public string Icon { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public int Amount { get; set; } = 0;

    [Required]
    public long Date { get; set; } = 0;
}
