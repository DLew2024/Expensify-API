using Expensify.API.DTOs.AccountDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.DataAccessLayer;
using Expensify.DataAccessLayer.Entities.Models;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses;

public class AccountService(ApplicationDbContext context) : IAccountService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Result<CreateAccountResponseDTO>> CreateAccount(
        Guid userId,
        CreateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var accountType = await _context
                .AccountTypes.AsNoTracking()
                .FirstOrDefaultAsync(
                    accountType =>
                        accountType.Id == request.AccountTypeId
                        && accountType.IsActive
                        && !accountType.IsDeleted
                        && (accountType.IsSystemDefault || accountType.UserId == userId),
                    cancellationToken
                );

            if (accountType == null)
            {
                return new Result<CreateAccountResponseDTO>(
                    new EntityNotFoundException(
                        "The selected account type could not be found or is unavailable."
                    )
                );
            }

            var currentDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var account = new Account
            {
                Name = request.Name.Trim(),
                UserId = userId,
                AccountTypeId = accountType.Id,

                InstitutionName = request.InstitutionName?.Trim(),
                LastFourDigits = request.LastFourDigits?.Trim(),

                CurrencyCode = request.CurrencyCode,

                CurrentBalance = request.InitialBalance,
                AvailableBalance = request.InitialBalance,

                IncludeInNetWorth = request.IncludeInNetWorth,

                IsActive = true,
                IsHidden = false,

                Notes = request.Notes?.Trim(),
                CreditLimit = request.CreditLimit,
                InterestRate = request.InterestRate,

                CreatedBy = userId,
                LastUpdatedBy = userId,
                UpdatedDate = currentDate,
            };

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateAccountResponseDTO
            {
                AccountId = account.Id,
                Name = account.Name,

                AccountTypeId = account.AccountTypeId,
                AccountTypeName = accountType.Name,

                InstitutionName = account.InstitutionName,
                LastFourDigits = account.LastFourDigits,

                CurrencyCode = account.CurrencyCode,

                CurrentBalance = account.CurrentBalance,
                AvailableBalance = account.AvailableBalance,

                IncludeInNetWorth = account.IncludeInNetWorth,

                IsActive = account.IsActive,
                IsHidden = account.IsHidden,

                Notes = account.Notes,
                CreditLimit = account.CreditLimit,
                InterestRate = account.InterestRate,

                CreateDate = account.CreateDate,
            };
        }
        catch (Exception ex)
        {
            return new Result<CreateAccountResponseDTO>(ex);
        }
    }
}
