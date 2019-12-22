using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace NotificationProcessor
{
    public static class NotificationProcessor
    {
        [FunctionName("NotificationProcessor")]
        public static void Run([TimerTrigger("* * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
