using CoverMe.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoverMe.Data.Data
{
    public class CoverMeDbContext : DbContext
    {
        public DbSet<NotificationRequest> NotificationRequests { get; set; }
        public DbSet<SentNotification> SentNotifiations { get; set; }
        public DbSet<TaskRecord> TaskRecords { get; set; }

        public CoverMeDbContext(DbContextOptions<CoverMeDbContext> options)
            : base(options)
        { }
    }
}
