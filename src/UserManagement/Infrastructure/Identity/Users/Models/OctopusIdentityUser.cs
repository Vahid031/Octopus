using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Octopus.UserManagement.Core.Identity.Users.Models;

public class OctopusIdentityUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<OtpCode> OtpCodes { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public bool OwnsToken(string token)
    {
        return RefreshTokens.Any(x => x.Token == token);
    }

    public bool IsValidCode(IOptions<JwtOptions> options, string code)
    {
        var OtpCode = OtpCodes.LastOrDefault();

        if (OtpCode is null) return false;

        if (!OtpCode.Code.Equals(code)) return false;

        if (OtpCode.IsExpired(options.Value.TokenDuration)) return false;

        return true;
    }
}

public class OtpCode
{
    public string Code { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int RetryCount { get; set; }
    public bool IsSignedIn { get; set; }

    public bool IsExpired(TimeSpan duration) => DateTimeOffset.UtcNow > CreatedAt.Add(duration);
}

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTimeOffset Expires { get; set; }
    public bool IsExpired => DateTimeOffset.UtcNow >= Expires;
    public DateTimeOffset Created { get; set; }
    public string CreatedByIp { get; set; }
    public DateTimeOffset? Revoked { get; set; }
    public string RevokedByIp { get; set; }
    public string ReplacedByToken { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}