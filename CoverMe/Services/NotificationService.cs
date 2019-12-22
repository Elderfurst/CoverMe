using System;
using CoverMe.Data;
using CoverMe.Models;
using CoverMe.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Linq;

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

            if (request.RainThreshold < 0 || request.RainThreshold > 100)
            {
                throw new Exception("Rain threshold needs to be between 0 and 100 (inclusive)");
            }

            await Db.NotificationRequests.AddAsync(request);

            await Db.SaveChangesAsync();
        }

        public async Task Unsubscribe(UnsubscribeRequest request)
        {
            // If somehow neither a phone number or email are specified then do nothing
            if (request.PhoneNumber == null && request.EmailAddress == null)
            {
                return;
            }

            // Remove any records matching the passed phone number
            if (request.PhoneNumber != null)
            {
                // Get the standard E.164 phone number parser
                var phoneNumberParser = PhoneNumbers.PhoneNumberUtil.GetInstance();

                var parsedPhoneNumber = phoneNumberParser.Parse(request.PhoneNumber.ToString(), request.PhoneNumberCountryCode);

                var phoneNumberMatches = Db.NotificationRequests.Where(
                    x => x.PhoneNumber == parsedPhoneNumber.NationalNumber 
                    && x.PhoneNumberCountryCode == request.PhoneNumberCountryCode);

                Db.NotificationRequests.RemoveRange(phoneNumberMatches);
            }

            // Remove any records matching the passed email address
            if (request.EmailAddress != null && IsValidEmail(request.EmailAddress))
            {
                var emailMatches = Db.NotificationRequests.Where(x => x.EmailAddress == request.EmailAddress);

                Db.NotificationRequests.RemoveRange(emailMatches);
            }

            // Commit the changes to the db
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
