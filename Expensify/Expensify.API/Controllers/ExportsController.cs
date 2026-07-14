using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

/// <summary>
/// Exports financial data.
/// Responsibilities:
/// - Export CSV
/// - Export Excel
/// - Export PDF
/// - Export transactions
/// - Export reports
/// - Export budgets
/// </summary>
public class ExportsController : AuthorizationControllerBase
{
    private readonly IService _service;

    public ExportsController(IService service)
    {
        _service = service;
    }
}
