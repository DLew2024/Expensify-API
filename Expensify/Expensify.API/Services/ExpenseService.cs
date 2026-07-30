using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.DTOs.ExpenseDTOs;
using Expensify.API.Services.Helpers;
using Expensify.API.Services.Interfaces;
using Expensify.API.Services.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Enums;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.Services;

public class ExpenseService(
    ApplicationDbContext context,
    IAccountResolver accountResolver,
    ITransactionResolver transactionResolver
) : IExpenseService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAccountResolver _accountResolver = accountResolver;
    private readonly ITransactionResolver _transactionResolver = transactionResolver;
    private readonly ILogger<ExpenseService> _logger;

    public async Task<Result<ExpenseTransactionResponseDTO>> AddExpense(
        Guid userId,
        AddExpenseTransactionDTO request,
        CancellationToken cancellationToken
    )
    {
        if (request.Amount <= 0)
        {
            return new Result<ExpenseTransactionResponseDTO>(
                new ValidationException("The expense amount must be greater than zero.")
            );
        }

        var userAccount = await _accountResolver.ResolveAccountByUserId(
            userId,
            request.AccountId,
            cancellationToken
        );

        if (userAccount is null)
        {
            return new Result<ExpenseTransactionResponseDTO>(
                new EntityNotFoundException("The requested account could not be found.")
            );
        }

        if (userAccount.ClosedDate.HasValue)
        {
            return new Result<ExpenseTransactionResponseDTO>(
                new ValidationException("Transactions cannot be added to a closed account.")
            );
        }

        if (request.PaymentMethodId.HasValue)
        {
            var paymentMethodExists = await _context.PaymentMethods.AnyAsync(
                paymentMethod =>
                    paymentMethod.Id == request.PaymentMethodId.Value && !paymentMethod.IsDeleted,
                cancellationToken
            );

            if (!paymentMethodExists)
            {
                return new Result<ExpenseTransactionResponseDTO>(
                    new EntityNotFoundException("The requested payment method could not be found.")
                );
            }
        }

        try
        {
            var newBalance = userAccount.CurrentBalance - request.Amount;

            var expenseTransaction = request.ToTransaction(userId, newBalance);

            userAccount.CurrentBalance = newBalance;
            userAccount.AvailableBalance = newBalance;

            _context.Transactions.Add(expenseTransaction);

            await _context.SaveChangesAsync(cancellationToken);

            return new Result<ExpenseTransactionResponseDTO>(
                ExpenseTransactionResponseDTO.FromTransaction(expenseTransaction)
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
                "A concurrency conflict occurred while adding expense for user {UserId} and account {AccountId}.",
                userId,
                request.AccountId
            );

            return new Result<ExpenseTransactionResponseDTO>(
                new ConflictException(
                    "The account was modified by another request. Please try again."
                )
            );
        }
        catch (DbUpdateException exception)
        {
            _logger.LogError(
                exception,
                "A database error occurred while adding expense for user {UserId} and account {AccountId}.",
                userId,
                request.AccountId
            );

            return new Result<ExpenseTransactionResponseDTO>(
                new Exception("The expense transaction could not be saved.")
            );
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "An error occurred while adding expense for user {UserId}.",
                userId
            );

            return new Result<ExpenseTransactionResponseDTO>(
                new Exception("The expense transaction could not be added.")
            );
        }
    }

    public async Task<Result<bool>> DeleteExpense(
        Guid userId,
        Guid expenseId,
        CancellationToken cancellationToken
    )
    {
        var expense = await _transactionResolver.ResolveTransactionByUserIdAndType(
            userId,
            expenseId,
            TransactionType.Expense,
            cancellationToken
        );

        if (expense is null)
        {
            return new Result<bool>(
                new EntityNotFoundException(
                    $"Expense transaction with id '{expenseId}' was not found."
                )
            );
        }

        var account = await _accountResolver.ResolveAccountByTransactionId(
            userId,
            expenseId,
            cancellationToken
        );

        if (account is null)
        {
            return new Result<bool>(
                new EntityNotFoundException($"Account with id '{expense.AccountId}' was not found.")
            );
        }

        TransactionHelper.SoftDeleteTransaction(expense, userId);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Result<byte[]>> DownloadExpenseExcel(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        // Find User Income based on id
        // Prepare Data for Excel

        // Catch error return 500
        throw new NotImplementedException();
    }

    public async Task<Result<List<TransactionDTO>>> GetAllExpense(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var resolvedAccountId = await _accountResolver.ResolveAccountIdByUserId(
                userId,
                accountId,
                cancellationToken
            );

            if (!resolvedAccountId.HasValue)
            {
                return new Result<List<TransactionDTO>>(
                    new EntityNotFoundException(
                        "The selected account could not be found or is unavailable."
                    )
                );
            }

            var expenseTransactions =
                await _transactionResolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
                    userId,
                    resolvedAccountId.Value,
                    TransactionType.Expense,
                    cancellationToken
                );

            return new Result<List<TransactionDTO>>(expenseTransactions);
        }
        catch (Exception ex)
        {
            return new Result<List<TransactionDTO>>(ex);
        }
    }
}
