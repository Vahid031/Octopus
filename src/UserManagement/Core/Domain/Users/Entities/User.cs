using Octopus.Core.Domain.Entities;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace Octopus.UserManagement.Core.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<OtpCode> OtpCodes { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActivated { get; set; }
    public bool OwnsToken(string token)
    {
        return RefreshTokens.Any(x => x.Token == token);
    }

    public bool IsValidCode(string code)
    {
        var OtpCode = OtpCodes.LastOrDefault();

        if (OtpCode is null) return false;

        if (!OtpCode.Code.Equals(code)) return false;

        //if (OtpCode.IsExpired(options.Value.TokenDuration)) return false;

        return true;
    }

    public void SetPassword(string password)
    {
        var iterations = 350000;
        var keySize = 32;
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            keySize);
        PasswordHash = Convert.ToBase64String(hash);
        PasswordSalt = Convert.ToHexString(hash);
    }

    public bool EqualWithPassword(string password)
    {
        var iterations = 350000;
        var keySize = 32;
        var salt = Convert.FromHexString(PasswordSalt);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            keySize);
        return Convert.ToBase64String(hash).Equals(PasswordHash);
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
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedByIp { get; set; }
    public DateTimeOffset? Revoked { get; set; }
    public string RevokedByIp { get; set; }
    public string ReplacedByToken { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}
