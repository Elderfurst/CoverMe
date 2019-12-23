using CoverMe.Data.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var timeSpan = lastRun?.TimeProcessed.TimeOfDay ?? new TimeSpan(0, 15, 0);

            var recordsToProcess = Db.NotificationRequests.Where(x => x.TimeToSend > timeSpan).ToListAsync();
        }

        private void SendEmails(IEnumerable<string> emailAddresses)
        {
            var smtpClient = new SmtpClient(SmtpCredential.HostName, SmtpCredential.Port);
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
    }
}
