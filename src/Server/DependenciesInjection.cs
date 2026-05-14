using Services;

namespace Server
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }
    }
}
