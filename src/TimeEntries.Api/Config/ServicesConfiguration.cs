using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeEntries.Api.Persistence;
using TimeEntries.Api.HealthChecks;

namespace TimeEntries.Api
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureDbContext(configuration)
                .ConfigureHealthChecks();

            return services;
        }

        private static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<TimeEntriesDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
		        .AddCheck<PendingDbMigrationHealthCheck<TimeEntriesDbContext>>("db-migration-check");

            return services;
        }
    }
}