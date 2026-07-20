using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.AccountDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

/// <summary>
/// Manages financial accounts.
/// Responsibilities:
/// - Create accounts
/// - Update accounts
/// - Delete/close accounts
/// - Get account details
/// - Get account balances
/// - Hide/unhide accounts
/// - Calculate net worth
/// </summary>
public class AccountsController : AuthorizationControllerBase
{
    private readonly IService _service;

    public AccountsController(IService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets all accounts for the authenticated user.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets an account by its unique identifier.
    /// </summary>
    [HttpGet("{accountId:guid}")]
    public async Task<IActionResult> GetAccountById(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a new financial account for the authenticated user.
    /// </summary>
    /// <returns>
    /// The newly created financial account.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<CreateAccountResponseDTO>> CreateAccount(
        [FromBody] CreateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AccountService.CreateAccount(
            CurrentUserId,
            request,
            cancellationToken
        );

        return result.Match<ActionResult<CreateAccountResponseDTO>>(
            success =>
                CreatedAtAction(
                    nameof(GetAccountById),
                    new { accountId = success.AccountId },
                    success
                ),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    EntityNotFoundException ex => NotFound(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    /// <summary>
    /// Updates an existing financial account.
    /// </summary>
    [HttpPut("{accountId:guid}")]
    public async Task<IActionResult> UpdateAccount(
        Guid accountId,
        [FromBody] UpdateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes or closes an account.
    /// </summary>
    [HttpDelete("{accountId:guid}")]
    public async Task<IActionResult> DeleteAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the current balance for an account.
    /// </summary>
    [HttpGet("{accountId:guid}/balance")]
    public async Task<IActionResult> GetAccountBalance(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Hides an account from normal account listings.
    /// </summary>
    [HttpPatch("{accountId:guid}/hide")]
    public async Task<IActionResult> HideAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Makes a hidden account visible again.
    /// </summary>
    [HttpPatch("{accountId:guid}/unhide")]
    public async Task<IActionResult> UnhideAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Closes an account while preserving its historical data.
    /// </summary>
    [HttpPatch("{accountId:guid}/close")]
    public async Task<IActionResult> CloseAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reopens a previously closed account.
    /// </summary>
    [HttpPatch("{accountId:guid}/reopen")]
    public async Task<IActionResult> ReopenAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the authenticated user's calculated net worth.
    /// </summary>
    [HttpGet("net-worth")]
    public async Task<IActionResult> GetNetWorth(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
