using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Expensify.API.Controllers
{
    /// <summary>
    /// Provides dashboard summaries and widgets.
    /// Responsibilities:
    /// - Account summaries
    /// - Current balances
    /// - Income vs expenses
    /// - Net worth
    /// - Recent transactions
    /// - Budget summaries
    /// - Upcoming bills
    /// - Spending overview
    /// </summary>
    public class DashboardController : AuthorizationControllerBase
    {
        private readonly IService _service;

        public DashboardController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDataResponseDTO>> GetDashboardData(
            Guid? accountId,
            CancellationToken cancellationToken
        )
        {
            var result = await _service.DashboardService.GetDashboardData(
                CurrentUserId,
                accountId,
                cancellationToken
            );
            return result.Match<ActionResult<DashboardDataResponseDTO>>(
                success => Ok(success),
                error =>
                    error switch
                    {
                        ValidationException ex => BadRequest(ex.Message),
                        UnauthorizedAccessException ex => Unauthorized(ex.Message),
                        _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                    }
            );
        }
    }
}
