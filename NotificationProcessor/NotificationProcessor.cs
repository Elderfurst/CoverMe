using CoverMe.Data.Data;
using CoverMe.Data.Models;
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
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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
        public async Task Run([TimerTrigger("0 * * * * *")]TimerInfo _)
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

                // Today's weather will always be the first entry in the daily list
                var weatherToday = weather.Daily.Data[0];

                var temp = weatherToday.PrecipProbability * 100;

                // Convert the precipitation probability to an int value for comparison
                // and make sure the threshold is less than the chance
                if (record.RainThreshold <= (weatherToday.PrecipProbability * 100))
                {
                    if (record.EmailAddress != null)
                    {
                        emailsToSend.Add(record.EmailAddress, weatherToday);
                    }

                    if (record.PhoneNumber != null)
                    {
                        // Get the standard E.164 phone number parser
                        var phoneNumberParser = PhoneNumberUtil.GetInstance();

                        var parsedPhoneNumber = phoneNumberParser.Parse(record.PhoneNumber.ToString(), record.PhoneNumberCountryCode.ToUpper());

                        textsToSend.Add(parsedPhoneNumber, weatherToday);
                    }
                }
            }

            // Send emails and texts where necessary
            await SendEmails(emailsToSend);
            SendTexts(textsToSend);

            // Save the record of this process
            var taskRecord = new TaskRecord
            {
                TimeProcessed = processTime,
                RecordsProcessed = recordsToProcess.Count,
            };

            await Db.TaskRecords.AddAsync(taskRecord);
            await Db.SaveChangesAsync();
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
            var accountSid = Environment.GetEnvironmentVariable("TwilioAccountSID");
            var authToken = Environment.GetEnvironmentVariable("TwilioAuthToken");
            var appPhoneNumber = Environment.GetEnvironmentVariable("TwilioPhoneNumber");

            TwilioClient.Init(accountSid, authToken);

            foreach (var record in records)
            {
                // Nothing needs to be done with the result, the message is sent when the Create method is called
                _ = MessageResource.Create(
                    body: BuildTextMessageBody(record.Value),
                    from: new Twilio.Types.PhoneNumber(appPhoneNumber),
                    // Phone number must include the country code being sent to
                    to: new Twilio.Types.PhoneNumber($"+{record.Key.CountryCode}{record.Key.NationalNumber}")
                );
            }
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

        private string BuildEmailBody(DailyData data)
        {
            return $"Precipitation is coming your way today! Probability: %{data.PrecipProbability * 100}";
        }

        private string BuildEmailSubject(DailyData data)
        {
            return $"Precipitation is coming your way today! Probability: %{data.PrecipProbability * 100}";
        }

        private string BuildTextMessageBody(DailyData data)
        {
            return $"Precipitation is coming your way today! Probability: %{data.PrecipProbability * 100}";
        }
    }
}
