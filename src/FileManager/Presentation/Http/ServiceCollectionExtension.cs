namespace Octopus.FileManager.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileManagerHttpServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);


        return services;
    }
}
