using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    /// <summary>
    /// Manages payment methods.
    /// Responsibilities:
    /// - Create payment methods
    /// - Update payment methods
    /// - Delete payment methods
    /// - Get available payment methods
    /// - Manage system defaults
    /// </summary>
    public class PaymentMethodsController : AuthorizationController
    {
        private readonly IService _service;

        public PaymentMethodsController(IService service)
        {
            _service = service;
        }

       
    }
}
