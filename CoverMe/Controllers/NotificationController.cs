using CoverMe.Models;
using CoverMe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoverMe.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService NotificationService;

        public NotificationController(INotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForNotifications(NotificationRequest request)
        {
            await NotificationService.AddNotificationRequest(request);

            return Ok();
        }
    }
}