using System;

namespace CoverMe.Models
{
    public class SentNotification
    {
        public int Id { get; set; }
        public int NotificationRequestId { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
