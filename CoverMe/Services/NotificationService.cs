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
            // Validate various pieces of the request
            if (request.EmailAddress == null && request.PhoneNumber == null)
            {
                throw new Exception("Either e-mail address or phone number needs to be supplied");
            }

            if (request.EmailAddress != null && !IsValidEmail(request.EmailAddress))
            {
                throw new Exception("Email address is improperly formatted");
            }

            if (request.PhoneNumber != null)
            {
                // Get the standard E.164 phone number parser
                var phoneNumberParser = PhoneNumbers.PhoneNumberUtil.GetInstance();

                var parsedPhoneNumber = phoneNumberParser.Parse(request.PhoneNumber.ToString(), request.PhoneNumberCountryCode);

                if (!phoneNumberParser.IsValidNumber(parsedPhoneNumber))
                {
                    throw new Exception("Phone number is improperly formatted");
                }
            }

            if (request.RainThreshold < 0 || request.RainThreshold > 100)
            {
                throw new Exception("Rain threshold needs to be between 0 and 100 (inclusive)");
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
