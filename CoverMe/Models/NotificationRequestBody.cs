﻿using System;

namespace CoverMe.Models
{
    public class NotificationRequestBody
    {
        public string PhoneNumber { get; set; }
        public string PhoneNumberCountryCode { get; set; }
        public string EmailAddress { get; set; }
        public DateTime TimeToSend { get; set; }
        public string TimeZoneId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int RainThreshold { get; set; }
    }
}
