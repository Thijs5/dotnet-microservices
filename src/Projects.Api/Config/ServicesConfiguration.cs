using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projects.Api.Persistence;
using Projects.Api.HealthChecks;

namespace Projects.Api
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
            services.AddDbContext<ProjectsDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
		        .AddCheck<PendingDbMigrationHealthCheck<ProjectsDbContext>>("db-migration-check");

            return services;
        }
    }
}