using CoverMe.Models;
using System.Threading.Tasks;

namespace CoverMe.Services.Interfaces
{
    public interface INotificationService
    {
        Task AddNotificationRequest(NotificationRequest request);
        Task Unsubscribe(UnsubscribeRequest request);
    }
}
