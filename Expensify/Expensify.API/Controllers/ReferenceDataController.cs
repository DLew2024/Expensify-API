using Expensify.API.DTOs.ReferenceDataDTOs;
using Expensify.API.Services.Interfaces;
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
        var result = await _service.ReferenceDataService.GetCurrencies(cancellationToken);

        return result.Match<ActionResult<CurrencyCodeDTO[]>>(
            success => Ok(success),
            error => StatusCode(StatusCodes.Status500InternalServerError, error.Message)
        );
    }

    [HttpGet("account-types")]
    public async Task<ActionResult<AccountTypeDTO[]>> GetAccountTypes(
        CancellationToken cancellationToken
    )
    {
        var result = await _service.ReferenceDataService.GetAccountTypes(
            CurrentUserId,
            cancellationToken
        );

        return result.Match<ActionResult<AccountTypeDTO[]>>(
            success => Ok(success),
            error => StatusCode(StatusCodes.Status500InternalServerError, error.Message)
        );
    }

    [HttpGet("payment-methods")]
    public async Task<ActionResult<PaymentMethodDTO[]>> GetPaymentMethods(
        CancellationToken cancellationToken
    )
    {
        var result = await _service.ReferenceDataService.GetPaymentMethods(
            CurrentUserId,
            cancellationToken
        );

        return result.Match<ActionResult<PaymentMethodDTO[]>>(
            success => Ok(success),
            error => StatusCode(StatusCodes.Status500InternalServerError, error.Message)
        );
    }

    [HttpGet("categories")]
    public async Task<ActionResult<CategoryDTO[]>> GetCategories(
        CancellationToken cancellationToken
    )
    {
        var result = await _service.ReferenceDataService.GetCategories(
            CurrentUserId,
            cancellationToken
        );

        return result.Match<ActionResult<CategoryDTO[]>>(
            success => Ok(success),
            error => StatusCode(StatusCodes.Status500InternalServerError, error.Message)
        );
    }
}
