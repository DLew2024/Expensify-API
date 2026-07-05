using Expensify.Models;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpDelete(":{id}")]
        public void DeleteExpense()
        {

        }
    }
}
