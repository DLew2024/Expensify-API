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
public class AccountsController(IService service) : AuthorizationControllerBase
{
    private readonly IService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountResponseDTO>>> GetUserAccounts(
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AccountService.GetAccounts(CurrentUserId, cancellationToken);

        return result.Match<ActionResult<IEnumerable<AccountResponseDTO>>>(
            success => Ok(success),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpGet("{accountId:guid}")]
    public async Task<IActionResult> GetAccountById(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<AccountResponseDTO>> CreateAccount(
        [FromBody] CreateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AccountService.CreateAccount(
            CurrentUserId,
            request,
            cancellationToken
        );

        return result.Match<ActionResult<AccountResponseDTO>>(
            success =>
                CreatedAtAction(nameof(GetAccountById), new { accountId = success.Id }, success),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    EntityNotFoundException ex => NotFound(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpPut("{accountId:guid}")]
    public async Task<IActionResult> UpdateAccount(
        Guid accountId,
        [FromBody] UpdateAccountDTO request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{accountId:guid}")]
    public async Task<IActionResult> DeleteAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AccountService.DeleteAccount(
            CurrentUserId,
            accountId,
            cancellationToken
        );

        return result.Match<IActionResult>(
            success => NoContent(),
            error =>
                error switch
                {
                    EntityNotFoundException ex => NotFound(ex.Message),
                    ValidationException ex => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpGet("{accountId:guid}/balance")]
    public async Task<IActionResult> GetAccountBalance(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId:guid}/hide")]
    public async Task<IActionResult> HideAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId:guid}/unhide")]
    public async Task<IActionResult> UnhideAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId:guid}/close")]
    public async Task<IActionResult> CloseAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId:guid}/reopen")]
    public async Task<IActionResult> ReopenAccount(
        Guid accountId,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    [HttpGet("net-worth")]
    public async Task<IActionResult> GetNetWorth(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
