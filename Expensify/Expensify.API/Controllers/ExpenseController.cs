using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    public class ExpenseController : AuthorizationControllerBase
    {
        private readonly IService _service;

        public ExpenseController(IService service)
        {
            _service = service;
        }

        [HttpPost(Name = "AddExpense")]
        public void AddExpense() { }

        [HttpGet(Name = "GetAllExpenses")]
        public void GetAllExpenses() { }

        [HttpGet(Name = "DownloadExpenseExcel")]
        public void DownloadExpenseExcel() { }

        [HttpDelete(":{id}")]
        public void DeleteExpense() { }
    }
}
