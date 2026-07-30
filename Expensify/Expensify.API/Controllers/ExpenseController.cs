using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.DTOs.ExpenseDTOs;
using Expensify.API.Services.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    public class ExpenseController(IService service) : AuthorizationControllerBase
    {
        private readonly IService _service = service;

        [HttpPost("add")]
        //[ServiceFilter(typeof(ValidationFilter<AddExpenseTransactionDTO>))] // Must implement
        public async Task<ActionResult<ExpenseTransactionResponseDTO>> AddExpense(
            AddExpenseTransactionDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExpenseService.AddExpense(
                CurrentUserId,
                request,
                cancellationToken
            );

            return result.Match<ActionResult<ExpenseTransactionResponseDTO>>(
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
        public async Task<IActionResult> DownloadExpenseExcel(CancellationToken cancellationToken)
        {
            var result = await _service.ExpenseService.DownloadExpenseExcel(
                CurrentUserId,
                cancellationToken
            );

            return result.Match<IActionResult>(
                fileBytes =>
                    File(
                        fileBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "expense_details.xlsx"
                    ),
                error => StatusCode(StatusCodes.Status500InternalServerError, error.Message)
            );
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<TransactionDTO>>> GetAllExpenses(
            [FromQuery] Guid accountId,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExpenseService.GetAllExpense(
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

        [HttpDelete("{expenseId:guid}")]
        public async Task<IActionResult> DeleteExpense(
            Guid expenseId,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExpenseService.DeleteExpense(
                CurrentUserId,
                expenseId,
                cancellationToken
            );

            return result.Match<IActionResult>(
                _ => NoContent(),
                error =>
                    error switch
                    {
                        EntityNotFoundException ex => NotFound(ex.Message),
                        ValidationException ex => BadRequest(ex.Message),
                        _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                    }
            );
        }
    }
}
