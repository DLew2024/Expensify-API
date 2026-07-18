using System.Security.Claims;
using Expensify.API.DTOs.IncomeDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Expensify.API.Utility.Validators.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

public class IncomeController : AuthorizationControllerBase
{
    private readonly IService _service;

    public IncomeController(IService service)
    {
        _service = service;
    }

    [HttpPost("add", Name = "AddIncome")]
    [ServiceFilter(typeof(ValidationFilter<AddIncomeTransactionDTO>))]
    public async Task<ActionResult<IncomeTransactionResponseDTO>> AddIncome(
        AddIncomeTransactionDTO request,
        CancellationToken cancellationToken
    )
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _service.IncomeService.AddIncome(userId, request, cancellationToken);

        return result.Match<ActionResult<IncomeTransactionResponseDTO>>(
            success => StatusCode(StatusCodes.Status201Created, success),
            error =>
                error switch
                {
                    EntityNotFoundException => BadRequest(error.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpGet("downloadExcel", Name = "DownloadIncomeExcel")]
    public async Task<ActionResult<bool>> DownloadIncomeExcel(
        DownloadIncomeExcelDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.IncomeService.DownloadIncomeExcel(request, cancellationToken);
        return result.Match<ActionResult<bool>>(
            success => StatusCode(StatusCodes.Status200OK, success),
            error =>
                error switch
                {
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpGet("getAll", Name = "GetAllIncomeSource")]
    public async Task<ActionResult<bool>> GetAllIncome(
        GetAllIncomeDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.IncomeService.GetAllIncome(request, cancellationToken);
        return result.Match<ActionResult<bool>>(
            success => StatusCode(StatusCodes.Status200OK, success),
            error =>
                error switch
                {
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpDelete(":{id}", Name = "Delete Income")]
    public async Task<ActionResult<bool>> DeleteIncomeSource(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.IncomeService.DeleteIncome(id, cancellationToken);
        return result.Match<ActionResult<bool>>(
            success => StatusCode(StatusCodes.Status204NoContent, id),
            error =>
                error switch
                {
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }
}
