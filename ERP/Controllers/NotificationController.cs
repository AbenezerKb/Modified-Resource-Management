using ERP.Context;
using ERP.DTOs.Others;
using ERP.Models;
using ERP.Services.NotificationServices;
using ERP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee,Admin,Coordinator,OfficeEngineer")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public NotificationController(INotificationService notificationService, IUserService userService)
        {
            _notificationService = notificationService;
            _userService = userService;
        }


        [HttpGet("all")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<List<Notification>>> GetAll()
        {
            if (!_userService.UserRole.IsAdmin)
                return Forbid();

            List<Notification> notifications = await _notificationService.GetAllNotifications();

            return Ok(notifications);
        }



        [HttpGet]
        public async Task<ActionResult<List<Notification>>> Get()
        {
            List<Notification> notifications = await _notificationService.GetUserNotifications();

            return Ok(notifications);
        }

        [HttpGet("clear/{notificationId}")]
        public async Task<ActionResult<bool>> Clear(int notificationId)
        {
            var notification = await _notificationService.GetNotification(notificationId);

            if (_userService.UserRole.IsAdmin ||
                    _userService.Employee.EmployeeSiteId == notification.SiteId ||
                    _userService.Employee.EmployeeId == notification.EmployeeId)
                return Ok(await _notificationService.Clear(notificationId));

            return Forbid();

        }

        [HttpGet("now")]
        public async Task<ActionResult<string>> Now()
        {
            return Ok(DateTime.Now.ToString());
        }
    }
}
