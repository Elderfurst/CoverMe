using CoverMe.Models;
using CoverMe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> RegisterForNotifications(NotificationRequest requestBody, string timeZoneId, DateTime timeToSend)
        {
            // Take the time submitted and offset by the selected time zone
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert from the sent DateTime to a TimeSpan for saving
            requestBody.TimeToSend = TimeZoneInfo.ConvertTimeToUtc(timeToSend, timeZone).TimeOfDay;

            try
            {
                await NotificationService.AddNotificationRequest(requestBody);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}