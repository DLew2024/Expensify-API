using Expensify.API.DTOs.IncomeDTOs;
using LanguageExt.Common;

namespace Expensify.API.ServiceClasses.Interfaces
{
    public interface IIncomeService
    {
        Task<Result<IncomeTransactionResponseDTO>> AddIncome(
            Guid userid,
            AddIncomeTransactionDTO request,
            CancellationToken cancellationToken
        );
        Task<Result<bool>> GetAllIncome(
            GetAllIncomeDTO request,
            CancellationToken cancellationToken
        );
        Task<Result<bool>> DeleteIncome(Guid id, CancellationToken cancellationToken);
        Task<Result<bool>> DownloadIncomeExcel(
            DownloadIncomeExcelDTO request,
            CancellationToken cancellationToken
        );
    }
}
