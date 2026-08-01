using Expensify.API.DTOs.ReferenceDataDTOs;
using LanguageExt.Common;

namespace Expensify.API.Services.Interfaces;

public interface IReferenceDataService
{
    Task<Result<CurrencyCodeDTO[]>> GetCurrencies(CancellationToken cancellationToken);

    Task<Result<AccountTypeDTO[]>> GetAccountTypes(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<Result<PaymentMethodDTO[]>> GetPaymentMethods(
        Guid userId,
        CancellationToken cancellationToken
    );

    Task<Result<CategoryDTO[]>> GetCategories(Guid userId, CancellationToken cancellationToken);
}
