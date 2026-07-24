using Expensify.API.DTOs.ReferenceDataDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.DataAccessLayer;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Expensify.API.ServiceClasses;

public class ReferenceDataService(ApplicationDbContext context) : IReferenceDataService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<CurrencyCodeDTO[]>> GetCurrencies(CancellationToken cancellationToken)
    {
        try
        {
            var currencies = await _context
                .CurrencyCodes.AsNoTracking()
                .Where(currency => currency.IsActive && !currency.IsDeleted)
                .OrderBy(currency => currency.Code)
                .Select(CurrencyCodeDTO.Projection)
                .ToArrayAsync(cancellationToken);

            return currencies;
        }
        catch (Exception ex)
        {
            return new Result<CurrencyCodeDTO[]>(ex);
        }
    }

    public async Task<Result<AccountTypeDTO[]>> GetAccountTypes(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var accountTypes = await _context
                .AccountTypes.AsNoTracking()
                .Where(accountType =>
                    accountType.IsActive
                    && !accountType.IsDeleted
                    && (accountType.IsSystemDefault || accountType.UserId == userId)
                )
                .OrderBy(accountType => accountType.Name)
                .Select(AccountTypeDTO.Projection)
                .ToArrayAsync(cancellationToken);

            return accountTypes;
        }
        catch (Exception ex)
        {
            return new Result<AccountTypeDTO[]>(ex);
        }
    }

    public async Task<Result<PaymentMethodDTO[]>> GetPaymentMethods(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var paymentMethods = await _context
                .PaymentMethods.AsNoTracking()
                .Where(paymentMethod =>
                    !paymentMethod.IsDeleted
                    && (paymentMethod.IsSystemDefault || paymentMethod.UserId == userId)
                )
                .OrderBy(paymentMethod => paymentMethod.Name)
                .Select(PaymentMethodDTO.Projection)
                .ToArrayAsync(cancellationToken);

            return paymentMethods;
        }
        catch (Exception ex)
        {
            return new Result<PaymentMethodDTO[]>(ex);
        }
    }

    public async Task<Result<CategoryDTO[]>> GetCategories(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var categories = await _context
                .Categories.AsNoTracking()
                .Where(category =>
                    category.IsActive
                    && !category.IsDeleted
                    && (category.IsSystemDefault || category.UserId == userId)
                )
                .OrderBy(category => category.Name)
                .Select(CategoryDTO.Projection)
                .ToArrayAsync(cancellationToken);

            return categories;
        }
        catch (Exception ex)
        {
            return new Result<CategoryDTO[]>(ex);
        }
    }
}
