using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    /// <summary>
    /// Manages financial goal types.
    /// Responsibilities:
    /// - Create goal types
    /// - Update goal types
    /// - Delete goal types
    /// - Get available goal types
    /// - Manage system defaults
    /// </summary>
    public class GoalTypesController : AuthorizationController
    {
        private readonly IService _service;

        public GoalTypesController(IService service)
        {
            _service = service;
        }

        
    }
}
