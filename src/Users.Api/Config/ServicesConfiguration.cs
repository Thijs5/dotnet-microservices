using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Api.Persistence;

namespace Users.Api
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDbContext(configuration);
            return services;
        }

        private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}