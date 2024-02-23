namespace Octopus.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);

        return services;
    }
}
