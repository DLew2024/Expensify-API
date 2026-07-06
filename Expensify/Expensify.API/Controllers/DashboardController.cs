using Expensify.API.ServicesClasses.Interfaces;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    [ApiController]
    [Route("")]
    public class DashboardController : ControllerBase
    {
        private readonly IService _service;

        public DashboardController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var dashboard = await _service.DashboardService.GetDashboardData(cancellationToken);
            return Ok(dashboard);
        }
    }
}
