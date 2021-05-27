using Microsoft.Extensions.DependencyInjection;
using Users.Api.Services;

namespace Users.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, FakeUserService>();
            return services;
        }
    }
}