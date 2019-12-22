using System;
using CoverMe.Data;
using CoverMe.Models;
using CoverMe.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Mail;

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
            if (request.EmailAddress == null && request.PhoneNumber == null)
            {
                throw new Exception("Either e-mail address or phone number needs to be supplied");
            }

            if (request.EmailAddress != null && !IsValidEmail(request.EmailAddress))
            {
                throw new Exception("Email address is improperly formatted");
            }

            await Db.NotificationRequests.AddAsync(request);

            await Db.SaveChangesAsync();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);

                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
