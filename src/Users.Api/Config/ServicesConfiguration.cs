using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Api.Persistence;
using Users.Api.HealthChecks;

namespace Users.Api
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
            var connectionString = configuration["SQL_CONNECTION_STRING"];
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
		        .AddCheck<PendingDbMigrationHealthCheck<UsersDbContext>>("db-migration-check");

            return services;
        }
    }
}