using Expensify.API.DTOs.ReferenceDataDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages reference data accounts.
/// Responsibilities:
///
/// </summary>
[Route("api/reference-data")]
public class ReferenceDataController(IService service) : AuthorizationControllerBase
{
    private readonly IService _service = service;

    [HttpGet("currencies")]
    public async Task<ActionResult<CurrencyCodeDTO[]>> GetCurrencies(
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpGet("account-types")]
    public async Task<ActionResult<AccountTypeDTO[]>> GetAccountTypes(
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpGet("payment-methods")]
    public async Task<ActionResult<PaymentMethodDTO[]>> GetPaymentMethods(
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpGet("categories")]
    public async Task<ActionResult<CategoryDTO[]>> GetCategories(
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
