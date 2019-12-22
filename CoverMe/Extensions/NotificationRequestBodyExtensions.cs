using CoverMe.Models;
using System;

namespace CoverMe.Extensions
{
    public static class NotificationRequestBodyExtensions
    {
        public static NotificationRequest ToNotificationRequest(this NotificationRequestBody requestBody)
        {
            // Get the standard E.164 phone number parser
            var phoneNumberParser = PhoneNumbers.PhoneNumberUtil.GetInstance();

            var parsedPhoneNumber = phoneNumberParser.Parse(requestBody.PhoneNumber, requestBody.PhoneNumberCountryCode);

            // Take the time submitted and offset by the selected time zone
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(requestBody.TimeZoneId);

            var temp =  new NotificationRequest
            {
                PhoneNumber = parsedPhoneNumber.NationalNumber,
                PhoneNumberCountryCode = requestBody.PhoneNumberCountryCode,
                EmailAddress = requestBody.EmailAddress,
                TimeToSend = TimeZoneInfo.ConvertTimeToUtc(requestBody.TimeToSend, timeZone).TimeOfDay,
                Latitude = requestBody.Latitude,
                Longitude = requestBody.Longitude,
                RainThreshold = requestBody.RainThreshold,
            };

            return temp;
        }
    }
}
