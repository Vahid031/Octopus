using Microsoft.Extensions.DependencyInjection;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Infrastructure.Mock.Users;

namespace Octopus.UserManagement.Infrastructure.Mock;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementMockDataServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, MockUserRepository>();

        return services;
    }
}
