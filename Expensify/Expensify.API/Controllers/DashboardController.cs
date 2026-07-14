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

        [HttpGet]
        public async Task<ActionResult<DashboardDataResponseDTO>> Get(
            CancellationToken cancellationToken
        )
        {
            var dashboard = await _service.DashboardService.GetDashboardData(cancellationToken);
            return Ok(dashboard);
        }
    }
}
