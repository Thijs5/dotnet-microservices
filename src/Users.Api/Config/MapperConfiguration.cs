using Microsoft.Extensions.DependencyInjection;
using Users.Api.Mappers;

namespace Users.Api
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IUserMapper, UserMapper>();
            return services;
        }
    }
}