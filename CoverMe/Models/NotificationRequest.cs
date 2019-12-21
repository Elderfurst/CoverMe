using System;

namespace CoverMe.Models
{
    public class NotificationRequest
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public TimeSpan TimeToSend { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int RainThreshold { get; set; }
    }
}
