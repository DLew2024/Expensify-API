using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    /// <summary>
    /// Generates financial reports and analytics.
    /// Responsibilities:
    /// - Spending reports
    /// - Income reports
    /// - Cash flow reports
    /// - Category reports
    /// - Merchant reports
    /// - Budget reports
    /// - Net worth history
    /// - Financial trends
    /// </summary>
    public class ReportsController : AuthorizationController
    {
        private readonly IService _service;

        public ReportsController(IService service)
        {
            _service = service;
        }
    }
}
