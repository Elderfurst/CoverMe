﻿using System;

namespace CoverMe.Data.Models
{
    public class NotificationRequest
    {
        public int Id { get; set; }
        public ulong? PhoneNumber { get; set; }
        public string PhoneNumberCountryCode { get; set; }
        public string EmailAddress { get; set; }
        public TimeSpan TimeToSend { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int RainThreshold { get; set; }
    }
}
