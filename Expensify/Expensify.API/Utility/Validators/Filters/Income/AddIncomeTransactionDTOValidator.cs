using Expensify.API.DTOs.IncomeDTOs;
using FluentValidation;

namespace Expensify.API.Utility.Validators.Filters.Income;

public class AddIncomeTransactionDTOValidator : AbstractValidator<AddIncomeTransactionDTO>
{
    private const int MaximumDescriptionLength = 100;
    private const int MaximumSourceNameLength = 100;
    private const int MaximumNotesLength = 1000;
    private const int MaximumTagLength = 50;

    public AddIncomeTransactionDTOValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("An account is required.");

        RuleFor(x => x.BudgetId)
            .NotEqual(Guid.Empty)
            .When(x => x.BudgetId.HasValue)
            .WithMessage("Budget ID must be valid.");

        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty)
            .When(x => x.CategoryId.HasValue)
            .WithMessage("Category ID must be valid.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Income amount must be greater than zero.");

        RuleFor(x => x.TransactionDate)
            .GreaterThan(0)
            .WithMessage("A valid transaction date is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(MaximumDescriptionLength)
            .WithMessage($"Description cannot exceed {MaximumDescriptionLength} characters.");

        RuleFor(x => x.Source)
            .NotEmpty()
            .WithMessage("Source name is required.")
            .MaximumLength(MaximumSourceNameLength)
            .WithMessage($"Source name cannot exceed {MaximumSourceNameLength} characters.");

        RuleFor(x => x.Notes)
            .MaximumLength(MaximumNotesLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Notes))
            .WithMessage($"Notes cannot exceed {MaximumNotesLength} characters.");

        RuleFor(x => x.PaymentMethodId)
            .NotEqual(Guid.Empty)
            .When(x => x.PaymentMethodId.HasValue)
            .WithMessage("Payment method ID must be valid.");

        RuleForEach(x => x.Tags)
            .NotEmpty()
            .WithMessage("Tags cannot be empty.")
            .MaximumLength(MaximumTagLength)
            .WithMessage($"Each tag cannot exceed {MaximumTagLength} characters.");

        RuleFor(x => x.Tags).Must(HaveUniqueTags).WithMessage("Tags must be unique.");
    }

    private static bool HaveUniqueTags(List<string> tags)
    {
        return tags.Distinct(StringComparer.OrdinalIgnoreCase).Count() == tags.Count;
    }
}
