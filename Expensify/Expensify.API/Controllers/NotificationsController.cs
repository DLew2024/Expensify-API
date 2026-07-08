using Expensify.API.DTOs.DashboardDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers
{
    /// <summary>
    /// Manages user notifications.
    /// Responsibilities:
    /// - Upcoming bills
    /// - Budget alerts
    /// - Large purchase alerts
    /// - Goal milestones
    /// - Recurring transaction reminders
    /// - Notification preferences
    /// </summary>
    public class NotificationsController : AuthorizationController
    {
        private readonly IService _service;

        public NotificationsController(IService service)
        {
            _service = service;
        }
    }
}
