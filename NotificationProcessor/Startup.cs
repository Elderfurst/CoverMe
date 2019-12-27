using NotificationProcessor;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using CoverMe.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(Startup))]
namespace NotificationProcessor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var databaseConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");
            
            builder.Services.AddDbContext<CoverMeDbContext>(options =>
            {
                SqlServerDbContextOptionsExtensions.UseSqlServer(options, databaseConnectionString);
            });
        }
    }
}
