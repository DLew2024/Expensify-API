namespace Expensify.API.Services.Interfaces

public interface IService
{
    IAuthService AuthService { get; }
    IDashboardService DashboardService { get; }
    IExpenseService ExpenseService { get; }
    IIncomeService IncomeService { get; }
}