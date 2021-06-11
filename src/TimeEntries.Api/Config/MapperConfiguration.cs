using Microsoft.Extensions.DependencyInjection;
using TimeEntries.Api.Mappers;

namespace TimeEntries.Api
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<ITimeEntryMapper, TimeEntryMapper>();
            return services;
        }
    }
}