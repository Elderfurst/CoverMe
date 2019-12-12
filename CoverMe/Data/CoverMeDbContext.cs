using CoverMe.Models;
using Microsoft.EntityFrameworkCore;

namespace CoverMe.Data
{
    public class CoverMeDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<NotificationRequest> NotificationRequests { get; set; }
        public DbSet<SentNotification> SentNotifiations { get; set; }

        public CoverMeDbContext(DbContextOptions<CoverMeDbContext> options)
            : base(options)
        { }
    }
}
