using Expensify.API.DTOs.IncomeDTOs;

namespace Expensify.API.ServiceClasses.Interfaces
{
    public interface IIncomeService
    {
        void AddIncome(AddIncomeDTO request, CancellationToken cancellationToken);
        void GetAllIncome(GetAllIncomeDTO request, CancellationToken cancellationToken);
        void DeleteIncome(Guid id, CancellationToken cancellationToken);
        void DownloadIncomeExcel(
            DownloadIncomeExcelDTO request,
            CancellationToken cancellationToken
        );
    }
}
