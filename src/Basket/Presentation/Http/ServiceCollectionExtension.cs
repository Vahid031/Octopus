namespace Octopus.Basket.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBasketHttpServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);


        return services;
    }
}
