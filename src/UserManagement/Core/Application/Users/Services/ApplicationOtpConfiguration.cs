using Microsoft.Extensions.Options;
using Octopus.UserManagement.Core.Application.Configurations.Options;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Services;

internal class ApplicationOtpConfiguration : IOtpConfiguration
{
    private readonly IOptions<OtpOptions> _options;

    public ApplicationOtpConfiguration(IOptions<OtpOptions> options)
    {
        _options = options;
    }

    public TimeSpan ExpireDuration => _options.Value.ExpireDuration;
    public int MaxRetryCount => _options.Value.MaxRetryCount;
}