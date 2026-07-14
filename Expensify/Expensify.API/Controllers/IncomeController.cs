using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    public class IncomeController : AuthorizationControllerBase
    {
        private readonly IService _service;

        public IncomeController(IService service)
        {
            _service = service;
        }

        [HttpPost(Name = "AddIncomeSource")]
        public void AddIncome() { }

        [HttpGet(Name = "GetAllIncomeSource")]
        public void GetAll() { }

        [HttpGet(Name = "DownloadIncomeExcel")]
        public void DownloadIncomeExcel() { }

        [HttpDelete(":{id}")]
        public void DeleteIncomeSource() { }
    }
}
