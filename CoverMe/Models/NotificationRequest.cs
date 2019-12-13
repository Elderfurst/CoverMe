using System;

namespace CoverMe.Models
{
    public class NotificationRequest
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime TimeToSend { get; set; }
        public string Timezone { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
