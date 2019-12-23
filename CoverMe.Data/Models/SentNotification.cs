using System;

namespace CoverMe.Data.Models
{
    public class SentNotification
    {
        public int Id { get; set; }
        public int NotificationRequestId { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
