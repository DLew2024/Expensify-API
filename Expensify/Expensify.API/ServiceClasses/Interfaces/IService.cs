using Expensify.API.Services;
namespace Expensify.API.ServicesClasses.Interfaces;

public interface IService
{
    IAuthService AuthService { get; }
    IDashboardService DashboardService { get; }
    IExpenseService ExpenseService { get; }
    IIncomeService IncomeService { get; }
}