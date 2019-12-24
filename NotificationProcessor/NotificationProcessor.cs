using CoverMe.Data.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotificationProcessor
{
    public class NotificationProcessor
    {
        private readonly CoverMeDbContext Db;
        private readonly SmtpCredential SmtpCredential;

        public NotificationProcessor(CoverMeDbContext db)
        {
            Db = db;
            SmtpCredential = LoadSmtpCredential();
        }

        [FunctionName("NotificationProcessor")]
        public async Task Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var processTime = DateTime.UtcNow;

            // Load most recent task run information
            var lastRun = await Db.TaskRecords.OrderByDescending(x => x.TimeProcessed).FirstOrDefaultAsync();

            // If no run is detected, just assume 15 minutes ago
            var timeSpan = lastRun?.TimeProcessed.TimeOfDay ?? processTime.TimeOfDay.Add(new TimeSpan(0, -15, 0));

            // Load any records that have send times after our last run up until right now
            var recordsToProcess = await Db.NotificationRequests.Where(x => x.TimeToSend > timeSpan && x.TimeToSend <= processTime.TimeOfDay).ToListAsync();

            var weatherService = new WeatherService();

            var emailsToSend = new Dictionary<string, DailyData>();
            var textsToSend = new Dictionary<PhoneNumber, DailyData>();
            
            foreach (var record in recordsToProcess)
            {
                var weather = await weatherService.GetWeatherByLocation(record.Latitude, record.Longitude);

                var weatherToday = weather.Daily.Data.FirstOrDefault(x => ConvertTimeToDateTime(x.Time).Date == processTime.Date);

                // Convert the precipitation probability to an int value for comparison
                if (record.RainThreshold >= (weatherToday.PrecipProbability * 100))
                {
                    if (record.EmailAddress != null)
                    {
                        emailsToSend.Add(record.EmailAddress, weatherToday);
                    }

                    if (record.PhoneNumber != null)
                    {
                        // Get the standard E.164 phone number parser
                        var phoneNumberParser = PhoneNumberUtil.GetInstance();

                        var parsedPhoneNumber = phoneNumberParser.Parse(record.PhoneNumber.ToString(), record.PhoneNumberCountryCode);

                        textsToSend.Add(parsedPhoneNumber, weatherToday);
                    }
                }
            }
        }

        private async Task SendEmails(Dictionary<string, DailyData> records)
        {
            var smtpClient = new SmtpClient(SmtpCredential.HostName, SmtpCredential.Port)
            {
                Credentials = new NetworkCredential(SmtpCredential.Username, SmtpCredential.Password),
            };

            foreach (var record in records)
            {
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress("coverme@nickanderson.dev"),
                    Body = BuildEmailBody(record.Value),
                    Subject = BuildEmailSubject(record.Value),
                };

                mailMessage.To.Add(new MailAddress(record.Key));

                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        private void SendTexts(Dictionary<PhoneNumber, DailyData> records)
        {

        }

        private SmtpCredential LoadSmtpCredential()
        {
            return new SmtpCredential
            {
                HostName = Environment.GetEnvironmentVariable("SmtpHostName"),
                Port = int.Parse(Environment.GetEnvironmentVariable("SmtpPort")),
                Username = Environment.GetEnvironmentVariable("SmtpUsername"),
                Password = Environment.GetEnvironmentVariable("SmtpPassword"),
            };
        }

        private DateTime ConvertTimeToDateTime(ulong time)
        {
            // Start at the unix beginning time to have an accurate datetime
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(time);
        }

        private string BuildEmailBody(DailyData data)
        {
            return string.Empty;
        }

        private string BuildEmailSubject(DailyData data)
        {
            return string.Empty;
        }
    }
}
