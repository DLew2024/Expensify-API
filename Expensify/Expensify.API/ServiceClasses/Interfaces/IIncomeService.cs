using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.DTOs.IncomeDTOs;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses.Interfaces
{
    public interface IIncomeService
    {
        Task<Result<IncomeTransactionResponseDTO>> AddIncome(
            Guid userId,
            AddIncomeTransactionDTO request,
            CancellationToken cancellationToken
        );
        public Task<Result<List<TransactionDTO>>> GetAllIncome(
            Guid userId,
            CancellationToken cancellationToken
        );

        Task<Result<bool>> DeleteIncome(
            Guid userId,
            Guid incomeId,
            CancellationToken cancellationToken
        );
        Task<Result<byte[]>> DownloadIncomeExcel(Guid userId, CancellationToken cancellationToken);
    }
}
