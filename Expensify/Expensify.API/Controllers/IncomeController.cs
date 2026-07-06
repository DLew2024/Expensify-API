using Expensify.API.ServicesClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
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
