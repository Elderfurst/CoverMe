﻿using CoverMe.Models;
using System;

namespace CoverMe.Extensions
{
    public static class NotificationRequestBodyExtensions
    {
        public static NotificationRequest ToNotificationRequest(this NotificationRequestBody requestBody)
        {
            // Take the time submitted and offset by the selected time zone
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(requestBody.TimeZoneId);

            return new NotificationRequest
            {
                PhoneNumber = requestBody.PhoneNumber?.ParsePhoneNumber(requestBody.PhoneNumberCountryCode),
                PhoneNumberCountryCode = requestBody.PhoneNumberCountryCode,
                EmailAddress = requestBody.EmailAddress,
                TimeToSend = TimeZoneInfo.ConvertTimeToUtc(requestBody.TimeToSend, timeZone).TimeOfDay,
                Latitude = requestBody.Latitude,
                Longitude = requestBody.Longitude,
                RainThreshold = requestBody.RainThreshold,
            };
        }
    }
}
