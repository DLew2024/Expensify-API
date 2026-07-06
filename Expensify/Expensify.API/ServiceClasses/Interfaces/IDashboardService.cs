using Microsoft.AspNetCore.Mvc;

namespace Expensify.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<IActionResult> GetDashboardData(CancellationToken cancellationToken);
    }
}
