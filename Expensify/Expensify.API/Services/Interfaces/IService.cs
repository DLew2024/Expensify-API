namespace Expensify.API.Services.Interfaces;

public interface IService
{
    IAuthService AuthService { get; }
    IDashboardService DashboardService { get; }
    IExpenseService ExpenseService { get; }
    IIncomeService IncomeService { get; }
    IJwtService JwtService { get; }
    IPasswordService PasswordService { get; }
    IAccountService AccountService { get; }
    IReferenceDataService ReferenceDataService { get; }
}
