namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
       
            return services;
        }
        public static WebApplication USerAPIServices(this WebApplication app)
        {
            return app;
        }
    }
}
