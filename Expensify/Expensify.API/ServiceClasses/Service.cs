using Expensify.API.ServiceClasses.Interfaces;
using Expensify.DataAccessLayer;
using Expensify.Services.Interfaces;

namespace Expensify.API.ServiceClasses
{
    public class Service(
        ApplicationDbContext context,
        IConfiguration configuration,
        IEmailService emailService,
        IPasswordService passwordService,
        ISecurityService securityService,
        IJwtService jwtService
    ) : IService
    {
        private readonly ApplicationDbContext _context =
            context ?? throw new ArgumentNullException(nameof(context));
        private readonly IJwtService _jwtService = jwtService;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ISecurityService _securityService = securityService;
        private readonly IEmailService _emailService = emailService;

        public IAuthService AuthService =>
            field
            ?? new AuthService(
                _context,
                _jwtService,
                _passwordService,
                _securityService,
                _emailService
            );
        public IDashboardService DashboardService => field ?? new DashboardService(_context);
        public IExpenseService ExpenseService => field ?? new ExpenseService(_context);
        public IIncomeService IncomeService => field ?? new IncomeService(_context);
        public IJwtService JwtService => field ?? new JwtService(configuration);
        public IPasswordService PasswordService => field ?? new PasswordService();
    }
}
