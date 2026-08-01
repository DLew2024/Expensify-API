using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.DTOs.IncomeDTOs;
using Expensify.API.ServiceClasses.Helpers;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Enums;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses;

public class IncomeService(
    ApplicationDbContext context,
    IAccountResolver accountResolver,
    ITransactionResolver transactionResolver
) : IIncomeService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAccountResolver _accountResolver = accountResolver;
    private readonly ITransactionResolver _transactionResolver = transactionResolver;

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

        var userAccount = await _accountResolver.ResolveAccountByUserIdWithTracking(
            userId,
            request.AccountId,
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

        if (request.PaymentMethodId.HasValue)
        {
            var paymentMethodExists = await _context.PaymentMethods.AnyAsync(
                paymentMethod =>
                    paymentMethod.Id == request.PaymentMethodId.Value && !paymentMethod.IsDeleted,
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
            userAccount.AvailableBalance = newBalance;

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
            return new Result<IncomeTransactionResponseDTO>(
                new ConflictException(
                    "The account was modified by another request. Please try again.", exception
                )
            );
        }
        catch (DbUpdateException exception)
        {
            return new Result<IncomeTransactionResponseDTO>(
                new Exception("The income transaction could not be saved.", exception)
            );
        }
        catch (Exception exception)
        {
            return new Result<IncomeTransactionResponseDTO>(
                new Exception("The income transaction could not be added.", exception)
            );
        }
    }

    public async Task<Result<bool>> DeleteIncome(
        Guid userId,
        Guid incomeId,
        CancellationToken cancellationToken
    )
    {
        var income = await _transactionResolver.ResolveTransactionByUserIdAndType(
            userId,
            incomeId,
            TransactionType.Income,
            cancellationToken
        );

        if (income is null)
        {
            return new Result<bool>(
                new EntityNotFoundException(
                    $"Income transaction with id '{incomeId}' was not found."
                )
            );
        }

        var account = await _accountResolver.ResolveAccountByTransactionId(
            userId,
            incomeId,
            cancellationToken
        );

        if (account is null)
        {
            return new Result<bool>(
                new EntityNotFoundException($"Account with id '{income.AccountId}' was not found.")
            );
        }

        TransactionHelper.SoftDeleteTransaction(income, userId);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
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

            var icomeTransactions =
                await _transactionResolver.ResolveTransactionsByUser_Account_Type_OrderedByDateDESC(
                    userId,
                    resolvedAccountId.Value,
                    TransactionType.Income,
                    cancellationToken
                );

            return new Result<List<TransactionDTO>>(icomeTransactions);
        }
        catch (Exception ex)
        {
            return new Result<List<TransactionDTO>>(ex);
        }
    }
}
