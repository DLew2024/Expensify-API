using Expensify.Models;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController(IService service) : ControllerBase
    {
        private readonly IService _service;

        public ExpenseController(IService service)
        {
            _service = service;
        }

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
