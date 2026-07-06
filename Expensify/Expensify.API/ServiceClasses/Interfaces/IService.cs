using Expensify.API.ServiceClasses.Interfaces;
using Expensify.Services.Interfaces;

namespace Expensify.API.ServicesClasses.Interfaces;

public interface IService
{
    IAuthService AuthService { get; }
    IDashboardService DashboardService { get; }
    IExpenseService ExpenseService { get; }
    IIncomeService IncomeService { get; }
    IJwtService JwtService { get; }
    IPasswordService PasswordService { get; }
}
