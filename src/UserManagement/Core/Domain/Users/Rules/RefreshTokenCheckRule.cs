using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class RefreshTokenCheckRule : IBusinessRule
{
    private readonly IEnumerable<RefreshTokenInfo> _refreshTokens;
    private readonly string _token;

    public RefreshTokenCheckRule(IEnumerable<RefreshTokenInfo> refreshTokens, string token)
    {
        _refreshTokens = refreshTokens;
        _token = token;
    }

    public void Validate()
    {
        var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token.Equals(_token));

        if (refreshToken is null)
            throw new RefreshTokenNotFoundException(_token);


        if (!refreshToken.IsActive)
            throw new RefreshTokenHasExpiredException(_token);
    }
}