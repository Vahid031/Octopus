using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.ValueObjects;

public class OtpCode : ValueObject<OtpCode>
{
	public string Code { get; private set; }
	public DateTimeOffset CreatedAt { get; private set; }
	public int RetryCount { get; private set; }
	public string CreatedByIp { get; private set; }
	public DateTimeOffset Expires { get; private set; }
	public DateTimeOffset? Revoked { get; private set; }
	public string RevokedByIp { get; private set; }

	public bool IsActive => Revoked == null && !IsExpired;


	private OtpCode(int retryCount, string createdByIp, TimeSpan expiresDuration, DateTimeOffset? revoked = null, string revokedByIp = null)
	{
		Code = Random.Shared.Next(10000, 99999).ToString();
		CreatedAt = DateTimeOffset.UtcNow;
		RetryCount = retryCount;
		CreatedByIp = createdByIp;
		Expires = DateTimeOffset.UtcNow.Add(expiresDuration);
		Revoked = revoked;
		RevokedByIp = revokedByIp;
	}

	// Todo Value object immutable
	public static OtpCode Create(int retryCount, string createdByIp,
		TimeSpan expiresDuration, DateTimeOffset? revoked = null, string revokedByIp = null) =>
		new OtpCode(retryCount, createdByIp, expiresDuration, revoked, revokedByIp);

	public bool IsExpired => DateTimeOffset.UtcNow >= Expires;

	public override bool ObjectIsEqual(OtpCode other)
	{
		if (other == null) return false;
		return Code == other.Code && CreatedAt == other.CreatedAt && Expires == other.Expires;
	}

	public override int ObjectGetHashCode()
	{
		return HashCode.Combine(Code, CreatedAt, RetryCount, CreatedByIp, Expires, Revoked, RevokedByIp);
	}
}