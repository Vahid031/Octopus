using Octopus.Core.Domain.Entities;

namespace Octopus.UserManagement.Core.Domain.Users.Entities;

public class RefreshToken : EntityBase
{
    #region Properties

    public string Token { get; private protected set; }

    public DateTimeOffset Expires { get; private protected set; }

    public DateTimeOffset CreatedAt { get; private protected set; }

    public string CreatedByIp { get; private protected set; }

    public DateTimeOffset? RevokedAt { get; private protected set; }

    public string RevokedByIp { get; private protected set; }

    public bool IsActive => RevokedAt == null && !IsExpired;

    public bool IsExpired => DateTimeOffset.UtcNow >= Expires;

    #endregion

    private RefreshToken(string refreshToken, DateTimeOffset expires, string createdByIp)
    {
        Token = refreshToken;
        Expires = expires;
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedByIp = createdByIp;
    }

    internal static RefreshToken Create(string refreshToken, DateTimeOffset expires, string createdByIp) =>
        new(refreshToken, expires, createdByIp);

    internal void Revoke(string revokedByIp)
    {
        RevokedByIp = revokedByIp;
        RevokedAt = DateTimeOffset.UtcNow;
    }
}