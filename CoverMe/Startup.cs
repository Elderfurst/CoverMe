using CoverMe.Configuration;
using CoverMe.Data.Data;
using CoverMe.Services;
using CoverMe.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoverMe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Bind app settings and register for dependency injection
            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            services.AddSingleton(appSettings);

            services.AddDbContext<CoverMeDbContext>(options =>
            {
                // Register the SQL Server connection string and set the migration assembly the main web app
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"), x => x.MigrationsAssembly("CoverMe"));
            });

            services.AddMemoryCache();

            // Register our services
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<INotificationService, NotificationService>();

            // Used for recompiling while working on the app
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
