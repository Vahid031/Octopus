namespace Octopus.Partner.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPartnerHttpServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);


        return services;
    }
}
