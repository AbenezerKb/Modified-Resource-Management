using ERP.Context;
using ERP.Models;
using ERP.Services.NotificationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class NotificationController : Controller
    {
        private readonly DataContext _context;
        private readonly INotificationService _notificationService;

        public NotificationController(DataContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Notification>>> GetAll()
        {
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
            var res = await _notificationService.Clear(notificationId);
            return Ok(res);
        }

        [HttpGet("now")]
        public async Task<ActionResult<string>> Now()
        {
            return Ok(DateTime.Now.ToString());
        }
    }
}
