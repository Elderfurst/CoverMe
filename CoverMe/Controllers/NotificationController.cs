﻿using CoverMe.Extensions;
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
        public async Task<IActionResult> RegisterForNotifications(NotificationRequestBody requestBody)
        {
            // Parse the request body into the correct model
            var notificationRequest = requestBody.ToNotificationRequest();

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
    }
}