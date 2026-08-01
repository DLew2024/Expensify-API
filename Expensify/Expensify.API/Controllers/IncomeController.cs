using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.DTOs.IncomeDTOs;
using Expensify.API.Services.Interfaces;
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

    [HttpPost("add")]
    [ServiceFilter(typeof(ValidationFilter<AddIncomeTransactionDTO>))]
    public async Task<ActionResult<IncomeTransactionResponseDTO>> AddIncome(
        AddIncomeTransactionDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.IncomeService.AddIncome(
            CurrentUserId,
            request,
            cancellationToken
        );

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

    [HttpGet("downloadExcel")]
    public async Task<IActionResult> DownloadIncomeExcel(CancellationToken cancellationToken)
    {
        var result = await _service.IncomeService.DownloadIncomeExcel(
            CurrentUserId,
            cancellationToken
        );

        return result.Match<IActionResult>(
            fileBytes =>
                File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "income_details.xlsx"
                ),
            error => StatusCode(StatusCodes.Status500InternalServerError, error.Message)
        );
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<List<TransactionDTO>>> GetAllIncome(
        [FromQuery] Guid accountId,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.IncomeService.GetAllIncome(
            CurrentUserId,
            accountId,
            cancellationToken
        );

        return result.Match<ActionResult<List<TransactionDTO>>>(
            success => Ok(success),
            error =>
                error switch
                {
                    EntityNotFoundException ex => NotFound(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [HttpDelete("{incomeId:guid}")]
    public async Task<IActionResult> DeleteIncome(
        Guid incomeId,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.IncomeService.DeleteIncome(
            CurrentUserId,
            incomeId,
            cancellationToken
        );

        return result.Match<IActionResult>(
            _ => NoContent(),
            error =>
                error switch
                {
                    EntityNotFoundException => NotFound(error.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }
}
