using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/", Name = "GetDashboardData")]
        public async Task<ActionResult<DashboardDataResponseDTO>> GetDashboardData(
            CancellationToken cancellationToken
        )
        {
            var result = await _service.DashboardService.GetDashboardData(
                CurrentUserId,
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
