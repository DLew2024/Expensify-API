using Expensify.Models;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    public class ExpenseController(IExpenseService service) : ControllerBase
    {
        [HttpPost(Name = "AddExpense")]
        public void AddExpense()
        {
            
        }

        [HttpGet(Name = "GetAllExpenses")]
        public void GetAllExpenses()
        {

        }

        [HttpGet(Name = "DownloadExpenseExcel")]
        public void DownloadExpenseExcel()
        {
        }

        [HttpDelete(Name = ":id")]
        public void DeleteExpense()
        {

        }
    }
}
