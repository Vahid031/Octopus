using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Enums;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    private User() { }

    private User(string username, string phoneNumber, string firstName, string lastName)
    {
        //ToDo: Check rules

        Username = username;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Roles = new List<RoleType>();
        OtpCodes = new List<OtpCode>();
        IsActivated = false;
    }

    public static User Create(string username, string phoneNumber, string firstName, string lastName)
    {
        return new User(username, phoneNumber, firstName, lastName);
    }

    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<OtpCode> OtpCodes { get; set; }
    public List<RoleType> Roles { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActivated { get; set; }

    public bool OwnsToken(string token)
    {
        return RefreshTokens.Any(x => x.Token == token);
    }

    public string CreateNewOtpCode(string ipAddress, TimeSpan expireDuration)
    {
        //ToDo: Check rules

        var otpCode = new OtpCode
        {
            CreatedAt = DateTimeOffset.UtcNow,
            Code = Random.Shared.Next(10000, 99999).ToString(),
            RetryCount = 0,
            CreatedByIp = ipAddress,
            Expires = DateTimeOffset.UtcNow.Add(expireDuration)
        };

        OtpCodes.Add(otpCode);

        return otpCode.Code;
    }

    private bool IsValidCode(string code)
    {
        //var OtpCode = OtpCodes.LastOrDefault();

        //if (OtpCode is null) return false;

        //if (!OtpCode.Code.Equals(code)) return false;

        //if (OtpCode.IsExpired(options.Value.TokenDuration)) return false;

        return true;
    }

    public TokenModel SignInWithPassword(IUserTokenGenerator tokenGenerator,
        IPasswordDomainService passwordService, string password, string ipAddress)
    {
        //ToDo: Check rules
        //passwordService.Equal(password, PasswordHash, PasswordSalt);

        return GenerateNewToken(tokenGenerator);
    }

    public TokenModel SignInWithOtp(IUserTokenGenerator tokenGenerator, string code, string ipAddress)
    {
        //ToDo: Check rules
        //Code exists


        return GenerateNewToken(tokenGenerator);
    }

    private TokenModel GenerateNewToken(IUserTokenGenerator tokenGenerator)
    {
        var tokenModel = tokenGenerator.GenerateToken(this);

        RefreshTokens.Add(new RefreshToken
        {
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByIp = tokenModel.IpAddress,
            Expires = tokenModel.RefreshTokenExpires,
            Token = tokenModel.RefreshToken
        });

        return tokenModel;
    }

    public void SetPassword(IPasswordDomainService passwordService, string password)
    {
        //ToDo: Check rules

        PasswordHash = passwordService.Hash(password, out var passwordSalt);
        PasswordSalt = passwordSalt;
    }

    public void ChangePassword(IPasswordDomainService passwordService, string oldPassword, string newPassword)
    {
        //ToDo: Check rules
        //passwordService.Equal(password, PasswordHash, PasswordSalt);

        PasswordHash = passwordService.Hash(newPassword, out var passwordSalt);
        PasswordSalt = passwordSalt;
    }

    public void Active()
    {
        //ToDo: Check rules

        IsActivated = true;
    }
}

public class OtpCode
{
    public string Code { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int RetryCount { get; set; }
    public string CreatedByIp { get; set; }
    public DateTimeOffset Expires { get; set; }
    public bool IsExpired => DateTimeOffset.UtcNow >= Expires;
    public DateTimeOffset? Revoked { get; set; }
    public string RevokedByIp { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}

public class RefreshToken
{
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
