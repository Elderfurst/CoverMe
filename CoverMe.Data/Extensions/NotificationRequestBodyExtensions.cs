using CoverMe.Data.Models;
using System;

namespace CoverMe.Data.Extensions
{
    public static class NotificationRequestBodyExtensions
    {
        public static NotificationRequest ToNotificationRequest(this NotificationRequestBody requestBody, TimeZoneInfo timeZone)
        {
            return new NotificationRequest
            {
                PhoneNumber = requestBody.PhoneNumber?.ParsePhoneNumber(requestBody.PhoneNumberCountryCode).NationalNumber,
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
