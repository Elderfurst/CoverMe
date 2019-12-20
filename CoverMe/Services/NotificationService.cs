using CoverMe.Data;
using CoverMe.Models;
using CoverMe.Services.Interfaces;
using System.Threading.Tasks;

namespace CoverMe.Services
{
    public class NotificationService : INotificationService
    {
        private readonly CoverMeDbContext Db;

        public NotificationService(CoverMeDbContext db)
        {
            Db = db;
        }
        public async Task AddNotificationRequest(NotificationRequest request)
        {
            await Db.NotificationRequests.AddAsync(request);
        }
    }
}
