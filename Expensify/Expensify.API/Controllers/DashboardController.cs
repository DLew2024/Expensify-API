using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    public class DashboardController : AuthorizationController
    {
        private readonly IService _service;

        public DashboardController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDataResponseDTO>> Get(CancellationToken cancellationToken)
        {
            var dashboard = await _service.DashboardService.GetDashboardData(cancellationToken);
            return Ok(dashboard);
        }
    }
}
