using CoverMe.Data.Extensions;
using CoverMe.Data.Models;
using CoverMe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoverMe.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService NotificationService;
        private readonly ILocationService LocationService;

        public NotificationController(INotificationService notificationService, ILocationService locationService)
        {
            NotificationService = notificationService;
            LocationService = locationService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForNotifications(NotificationRequestBody requestBody)
        {
            // Load the timezone for the selected location
            var timeZone = await LocationService.GetTimeZone(requestBody.Latitude, requestBody.Longitude);

            // Parse the request body into the correct model
            var notificationRequest = requestBody.ToNotificationRequest(timeZone);

            try
            {
                await NotificationService.AddNotificationRequest(notificationRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Unsubscribe(UnsubscribeRequest request)
        {
            await NotificationService.Unsubscribe(request);

            return Ok();
        }
    }
}
