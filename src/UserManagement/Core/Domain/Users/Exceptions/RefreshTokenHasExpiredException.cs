using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class RefreshTokenHasExpiredException : OctopusDomainException
{
    public RefreshTokenHasExpiredException(string refreshToken) : base("Refresh token: '{refreshToken}' has expired", refreshToken)
    {
    }
}