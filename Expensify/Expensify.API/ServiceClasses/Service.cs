using Expensify.API.Configurations;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.DataAccessLayer;
using Microsoft.Extensions.Options;

namespace Expensify.API.ServiceClasses
{
    public class Service(
        ApplicationDbContext context,
        IConfiguration configuration,
        IEmailService emailService,
        IPasswordService passwordService,
        ISecurityService securityService,
        IAccountResolver accountResolver,
        IAccountTypeResolver accountTypeResolver,
        ITransactionResolver transactionResolver,
        IJwtService jwtService,
        IOptions<FrontendSettings> frontendOptions,
        IOptions<JwtSettings> jwtOptions
    ) : IService
    {
        private readonly ApplicationDbContext _context =
            context ?? throw new ArgumentNullException(nameof(context));
        private readonly IJwtService _jwtService = jwtService;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ISecurityService _securityService = securityService;
        private readonly IEmailService _emailService = emailService;
        private readonly IAccountResolver _accountResolver = accountResolver;
        private readonly ITransactionResolver _transactionResolver = transactionResolver;
        private readonly IAccountTypeResolver _accountTypeResolver = accountTypeResolver;
        private readonly IOptions<FrontendSettings> _frontendSettings = frontendOptions;
        private readonly IOptions<JwtSettings> _jwtSettings = jwtOptions;

        public IAccountService AccountService =>
            field ?? new AccountService(_context, _accountTypeResolver);
        public IAuthService AuthService =>
            field
            ?? new AuthService(
                _context,
                _jwtService,
                _passwordService,
                _securityService,
                _emailService,
                _frontendSettings,
                _jwtSettings
            );
        public IDashboardService DashboardService =>
            field ?? new DashboardService(_accountResolver, _transactionResolver);
        public IExpenseService ExpenseService =>
            field ?? new ExpenseService(_context, _accountResolver, _transactionResolver);
        public IIncomeService IncomeService =>
            field ?? new IncomeService(_context, _accountResolver, _transactionResolver);
        public IReferenceDataService ReferenceDataService =>
            field ?? new ReferenceDataService(_context);
        public IJwtService JwtService => field ?? new JwtService(configuration);
        public IPasswordService PasswordService => field ?? new PasswordService();
    }
}
