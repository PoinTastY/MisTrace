using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MisTrace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MisTrace.ServiceDefaults
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            services.AddStores(configuration, enviroment);

            services.AddAuthorization();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddStores(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            string connectionString = Environment.GetEnvironmentVariable("MISTRACE_DB")
                ?? throw new InvalidOperationException("Environment variable 'MISTRACE_DB' not set.");

            services.AddDbContext<MisTraceDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
