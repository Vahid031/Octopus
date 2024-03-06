using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.UserManagement.Core.Application.Configurations.Options;

namespace Octopus.UserManagement.Core.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementApplicationServices(this IServiceCollection services, IConfiguration configuration, string otpSectionName = "otp")
    {
        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        services.Configure<OtpOptions>(configuration.GetSection(otpSectionName));

        return services;
    }
}
