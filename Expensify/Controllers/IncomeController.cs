using Expensify.Models;
using Expensify.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    public class IncomeController(IIncomeService service) : ControllerBase
    {
        [HttpPost(Name = "AddIncomeSource")]
        public void AddIncome()
        {
           
        }

        [HttpGet(Name = "GetAllIncomeSource")]
        public void GetAll()
        {
            
        }

        [HttpGet(Name = "DownloadIncomeExcel")]
        public void DownloadIncomeExcel()
        {
        }

        [HttpDelete(Name = ":id")]
        public void DeleteIncomeSource()
        {

        }
    }
}
