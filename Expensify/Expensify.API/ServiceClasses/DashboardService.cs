using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses;

public class DashboardService(
    ApplicationDbContext context,
    IAccountResolver accountResolver,
    ITransactionResolver transactionResolver
) : IDashboardService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAccountResolver _accountResolver = accountResolver;
    private readonly ITransactionResolver _transactionResolver = transactionResolver;

    public async Task<Result<DashboardDataResponseDTO>> GetDashboardData(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var resolvedAccount = await _accountResolver.ResolveAccountByUserId(
                userId,
                accountId,
                cancellationToken
            );

            if (resolvedAccount is null)
            {
                return new Result<DashboardDataResponseDTO>(
                    new EntityNotFoundException(
                        "The selected account could not be found or is unavailable."
                    )
                );
            }

            var accountDetails = new AccountSummaryDTO
            {
                Id = resolvedAccount.Id,
                Name = resolvedAccount.Name,
                CurrentBalance = resolvedAccount.CurrentBalance,
            };

            var transactions = await _transactionResolver.ResolveTransactionsByUserAndAccount(
                userId,
                resolvedAccount.Id,
                cancellationToken
            );

            var response = DashboardDataResponseDTO.Create(accountDetails, transactions);

            return new Result<DashboardDataResponseDTO>(response);
        }
        catch (Exception exception)
        {
            return new Result<DashboardDataResponseDTO>(exception);
        }
    }
}
