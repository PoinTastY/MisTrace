using CrewDo.Infrastructure.Services.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MisTrace.Domain.Entities;
using MisTrace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MisTrace.ServiceDefaults
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            services.AddPersistency(configuration, enviroment);

            services.AddAuthorization();

            services.AddIdentityApiEndpoints<MisTraceUser>()
                .AddEntityFrameworkStores<MisTraceDbContext>();

            //Email service, required by Identity
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }

        private static IServiceCollection AddPersistency(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            string connectionString = enviroment.IsDevelopment()
                ? Environment.GetEnvironmentVariable("CREWDO_DB_DEV") ?? throw new InvalidOperationException("Environment variable 'AUTH_DB_DEV' not set.")
                : Environment.GetEnvironmentVariable("CREWDO_DB_PROD") ?? throw new InvalidOperationException("Environment variable 'AUTH_DB_PROD' not set.");

            services.AddDbContext<MisTraceDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
