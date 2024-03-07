using Microsoft.Extensions.DependencyInjection;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordDomainService, Sha256PasswordDomainService>();

        return services;
    }
}
