namespace Octopus.Catalog.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementHttpServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);


        return services;
    }
}
