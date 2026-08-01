using Expensify.API.DTOs.DashboardDTOs;
using LanguageExt.Common;

namespace Expensify.API.Services.Interfaces;

public interface IDashboardService
{
    Task<Result<DashboardDataResponseDTO>> GetDashboardData(
        Guid userId,
        Guid accountId,
        CancellationToken cancellationToken
    );
}
