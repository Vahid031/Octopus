using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.ValueObjects;

public class RefreshToken : ValueObject<RefreshToken>
{
	public string Token { get; private set; }
	public DateTimeOffset Expires { get; private set; }
	public DateTimeOffset CreatedAt { get; private set; }
	public string CreatedByIp { get; private set; }
	public DateTimeOffset? Revoked { get; private set; }
	public string RevokedByIp { get; private set; }
	public string ReplacedByToken { get; private set; }

	public bool IsActive => Revoked == null && !IsExpired;
	public bool IsExpired => DateTimeOffset.UtcNow >= Expires;

	private RefreshToken(string token, DateTimeOffset expires, DateTimeOffset createdAt, string createdByIp, string replacedByToken = null, DateTimeOffset? revoked = null, string revokedByIp = null)
	{
		Token = token;
		Expires = expires;
		CreatedAt = createdAt;
		CreatedByIp = createdByIp;
		Revoked = revoked;
		RevokedByIp = revokedByIp;
		ReplacedByToken = replacedByToken;
	}

	// Todo Value object immutable
	public static RefreshToken Create(string token, DateTimeOffset expires, DateTimeOffset createdAt, string createdByIp,
		string replacedByToken = null, DateTimeOffset? revoked = null, string revokedByIp = null) =>
		new RefreshToken(token, expires, createdAt, createdByIp, replacedByToken, revoked, revokedByIp);

	public override bool ObjectIsEqual(RefreshToken other)
	{
		if (other == null) return false;
		return Token == other.Token && Expires == other.Expires && CreatedAt == other.CreatedAt;
	}

	public override int ObjectGetHashCode()
	{
		return HashCode.Combine(Token, Expires, CreatedAt, CreatedByIp, Revoked, RevokedByIp, ReplacedByToken);
	}
}