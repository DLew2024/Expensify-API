using Expensify.API.ServiceClasses.Interfaces;

namespace Expensify.API.Controllers
{
    /// <summary>
    /// Manages financial transactions.
    /// Responsibilities:
    /// - Create income
    /// - Create expenses
    /// - Transfer money between accounts
    /// - Edit transactions
    /// - Delete transactions
    /// - Search transactions
    /// - Filter transactions
    /// - View transaction history
    /// - Attach tags and notes
    /// </summary>
    public class TransactionsController : AuthorizationController
    {
        private readonly IService _service;

        public TransactionsController(IService service)
        {
            _service = service;
        }
    }
}
