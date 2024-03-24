namespace Octopus.Checkout.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCheckoutHttpServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);


        return services;
    }
}
