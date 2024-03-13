using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class RefreshTokenNotFoundException : OctopusDomainException
{
    public RefreshTokenNotFoundException(string refreshToken) : base("Refresh token: '{refreshToken}' not found", refreshToken)
    {
    }
}