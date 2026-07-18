using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.DTOs.IncomeDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Enums;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses;

public class IncomeService(ApplicationDbContext context) : IIncomeService
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<IncomeService> _logger;

    public async Task<Result<IncomeTransactionResponseDTO>> AddIncome(
        Guid userId,
        AddIncomeTransactionDTO request,
        CancellationToken cancellationToken
    )
    {
        if (request.Amount <= 0)
        {
            return new Result<IncomeTransactionResponseDTO>(
                new ValidationException("The income amount must be greater than zero.")
            );
        }

        var userAccount = await _context.Accounts.FirstOrDefaultAsync(
            account =>
                account.Id == request.AccountId && account.UserId == userId && !account.IsDeleted,
            cancellationToken
        );

        if (userAccount is null)
        {
            return new Result<IncomeTransactionResponseDTO>(
                new EntityNotFoundException("The requested account could not be found.")
            );
        }

        if (userAccount.ClosedDate.HasValue)
        {
            return new Result<IncomeTransactionResponseDTO>(
                new ValidationException("Transactions cannot be added to a closed account.")
            );
        }

        if (request.BudgetId.HasValue)
        {
            var canUseBudget = await _context.Budgets.AnyAsync(
                budget =>
                    budget.Id == request.BudgetId.Value
                    && budget.IsActive
                    && !budget.IsDeleted
                    && (
                        budget.OwnerUserId == userId
                        || budget.Members.Any(member =>
                            member.UserId == userId
                            && !member.IsDeleted
                            && (
                                member.Role == BudgetMemberRole.Admin
                                || member.Role == BudgetMemberRole.Editor
                            )
                        )
                    ),
                cancellationToken
            );

            if (!canUseBudget)
            {
                return new Result<IncomeTransactionResponseDTO>(
                    new EntityNotFoundException("The requested budget could not be found.")
                );
            }
        }

        if (request.CategoryId.HasValue)
        {
            var categoryExists = await _context.Categories.AnyAsync(
                category =>
                    category.Id == request.CategoryId.Value
                    && !category.IsDeleted
                    && category.IsActive
                    && category.Type == CategoryType.Income
                    && (category.UserId == userId || category.IsSystemDefault),
                cancellationToken
            );

            if (!categoryExists)
            {
                return new Result<IncomeTransactionResponseDTO>(
                    new EntityNotFoundException("The requested category could not be found.")
                );
            }
        }

        if (request.PaymentMethodId.HasValue)
        {
            var paymentMethodExists = await _context.PaymentMethods.AnyAsync(
                paymentMethod =>
                    paymentMethod.Id == request.PaymentMethodId.Value
                    && paymentMethod.UserId == userId
                    && !paymentMethod.IsDeleted,
                cancellationToken
            );

            if (!paymentMethodExists)
            {
                return new Result<IncomeTransactionResponseDTO>(
                    new EntityNotFoundException("The requested payment method could not be found.")
                );
            }
        }

        try
        {
            var newBalance = userAccount.CurrentBalance + request.Amount;

            var incomeTransaction = request.ToTransaction(userId, newBalance);

            userAccount.CurrentBalance = newBalance;

            _context.Transactions.Add(incomeTransaction);

            await _context.SaveChangesAsync(cancellationToken);

            return new Result<IncomeTransactionResponseDTO>(
                IncomeTransactionResponseDTO.FromTransaction(incomeTransaction)
            );
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (DbUpdateConcurrencyException exception)
        {
            _logger.LogWarning(
                exception,
                "A concurrency conflict occurred while adding income for user {UserId} and account {AccountId}.",
                userId,
                request.AccountId
            );

            return new Result<IncomeTransactionResponseDTO>(
                new ConflictException(
                    "The account was modified by another request. Please try again."
                )
            );
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError(
                exception,
                "A database error occurred while adding income for user {UserId} and account {AccountId}.",
                userId,
                request.AccountId
            );

            return new Result<IncomeTransactionResponseDTO>(
                new Exception("The income transaction could not be saved.")
            );
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "An error occurred while adding income for user {UserId}.",
                userId
            );

            return new Result<IncomeTransactionResponseDTO>(
                new Exception("The income transaction could not be added.")
            );
        }
    }

    public Task<Result<bool>> DeleteIncome(
        Guid userId,
        Guid incomeId,
        CancellationToken cancellationToken
    )
    {
        // Find and delete income by id
        // Return message to indicate success

        // Catch error return 500
        throw new NotImplementedException();
    }

    public async Task<Result<byte[]>> DownloadIncomeExcel(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        //try
        //{
        //    // Generate your Excel file...

        //    //byte[] fileBytes = /* generated Excel bytes */;

        //    return new Result<byte[]>(fileBytes);
        //}
        //catch (Exception ex)
        //{
        //    return new Result<byte[]>(ex);
        //}

        throw new NotImplementedException();
    }

    public async Task<Result<List<TransactionDTO>>> GetAllIncome(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var transactions = await _context
                .Transactions.AsNoTracking()
                .Where(transaction =>
                    transaction.UserId == userId
                    && transaction.Type == TransactionType.Income
                    && !transaction.IsDeleted
                )
                .OrderByDescending(transaction => transaction.TransactionDate)
                .Select(TransactionDTO.Projection)
                .ToListAsync(cancellationToken);

            return new Result<List<TransactionDTO>>(transactions);
        }
        catch (Exception ex)
        {
            return new Result<List<TransactionDTO>>(ex);
        }
    }
}
