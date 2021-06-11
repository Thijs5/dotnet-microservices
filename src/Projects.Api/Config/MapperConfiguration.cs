using Microsoft.Extensions.DependencyInjection;
using Projects.Api.Mappers;

namespace Projects.Api
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IProjectMapper, ProjectMapper>();
            return services;
        }
    }
}