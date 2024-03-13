using Octopus.Core.Domain.Entities;

namespace Octopus.UserManagement.Core.Domain.Users.Entities;

public class OtpCode : EntityBase
{
    #region Properties
    public string Code { get; private protected set; }

    public DateTimeOffset CreatedAt { get; private protected set; }

    public int RetryCount { get; private protected set; }

    public string CreatedByIp { get; private protected set; }

    public DateTimeOffset? RevokedAt { get; private protected set; }

    public string RevokedByIp { get; private protected set; }

    public bool IsRevoked => RevokedAt != null;

    #endregion

    private OtpCode() { }

    private OtpCode(string createdByIp)
    {
        Code = Random.Shared.Next(10000, 99999).ToString();
        CreatedAt = DateTimeOffset.UtcNow;
        RetryCount = 0;
        CreatedByIp = createdByIp;
    }

    internal static OtpCode Create(string createdByIp) =>
        new(createdByIp);


    internal void Revoke(string revokedByIp)
    {
        RevokedAt = DateTimeOffset.UtcNow;
        RevokedByIp = revokedByIp;
    }

    internal void Retry() => RetryCount++;
}