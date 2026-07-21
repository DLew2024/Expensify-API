using System.Security.Claims;
using Expensify.API.DTOs.ExpenseDTOs;
using Expensify.API.DTOs.IncomeDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using LanguageExt.ClassInstances;
using LanguageExt.Pipes;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    public class ExpenseController : AuthorizationControllerBase
    {
        private readonly IService _service;

        public ExpenseController(IService service)
        {
            _service = service;
        }

        [HttpPost("add", Name = "AddExpense")]
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

        [HttpGet("downloadExcel", Name = "DownloadExpenseExcel")]
        public async Task<ActionResult<bool>> DownloadExpenseExcel(
            DownloadExpenseExcelDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExpenseService.DownloadExpenseExcel(
                request,
                cancellationToken
            );
            return result.Match<ActionResult<bool>>(
                success => StatusCode(StatusCodes.Status200OK, success),
                error =>
                    error switch
                    {
                        _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                    }
            );
        }

        [HttpGet("getAll", Name = "GetAllExpenses")]
        public async Task<ActionResult<bool>> GetAllExpenses(
            GetExpenseIncomeDTO request,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExpenseService.GetAllExpense(request, cancellationToken);
            return result.Match<ActionResult<bool>>(
                success => StatusCode(StatusCodes.Status200OK, success),
                error =>
                    error switch
                    {
                        _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                    }
            );
        }

        [HttpDelete(":{id}", Name = "Delete Expense")]
        public async Task<ActionResult<bool>> DeleteExpense(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.ExpenseService.DeleteExpense(id, cancellationToken);
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
}
