using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Enums;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Rules;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    #region Properties

    public string UserName { get; private protected set; }

    public Password Password { get; private protected set; }

    public PhoneNumber PhoneNumber { get; private protected set; }

    public string FirstName { get; private protected set; }

    public string LastName { get; private protected set; }

    public DateTimeOffset CreatedAt { get; private protected set; }

    public bool IsActivated { get; private protected set; }

    public List<OtpCode> OtpCodes { get; private protected set; }

    public List<RefreshToken> RefreshTokens { get; private protected set; }

    public List<RoleType> Roles { get; private protected set; }

    #endregion

    private User() { }

    private User(IUserRepository userRepository, UserId id, string userName, PhoneNumber phoneNumber, string firstName, string lastName)
    {
        CheckRule(new UserNameMustBeUniqueRule(userRepository, userName));
        CheckRule(new UserNameMustBeAtLeast3CharacterRule(userName));
        CheckRule(new UserNameMustBeAtLast150CharacterRule(userName));

        CheckRule(new UserFirstNameMustBeAtLeast2CharacterRule(firstName));
        CheckRule(new UserFirstNameMustBeAtLast50CharacterRule(firstName));

        CheckRule(new UserLastNameMustBeAtLeast2CharacterRule(lastName));
        CheckRule(new UserLastNameMustBeAtLast50CharacterRule(lastName));

        UserName = userName;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        IsActivated = false;
        CreatedAt = DateTimeOffset.UtcNow;
        Id = id;

        Roles = new List<RoleType>();
        OtpCodes = new List<OtpCode>();
        RefreshTokens = new List<RefreshToken>();
    }

    public static User Create(IUserRepository userRepository, string userName, string phoneNumber, string firstName, string lastName)
    {
        return new User(userRepository, UserId.New(), userName, phoneNumber, firstName, lastName);
    }

    public OtpModel CreateNewOtpCode(IOtpConfiguration otpConfiguration, string ipAddress)
    {
        CheckRule(new SendOtpCodeRule(OtpCodes));

        var otpCode = OtpCode.Create(ipAddress);

        OtpCodes.Add(otpCode);

        return new OtpModel
        {
            Code = otpCode.Code,
            Expires = otpCode.CreatedAt.Add(otpConfiguration.ExpireDuration)
        };
    }

    public TokenModel SignInWithPassword(IUserTokenGenerator tokenGenerator,
        IPasswordDomainService passwordService, string password, string ipAddress)
    {
        CheckRule(new PasswordCompareRule(passwordService, password, Password));

        return GenerateNewToken(tokenGenerator, ipAddress);
    }

    public TokenModel SignInWithOtp(IUserTokenGenerator tokenGenerator,
        IOtpConfiguration otpConfiguration, string code, string ipAddress)
    {
        var otpCode = OtpCodes.LastOrDefault();
        otpCode?.Retry();

        CheckRule(new OtpCodeCheckRule(otpConfiguration, otpCode, code));

        otpCode!.Revoke(ipAddress);

        return GenerateNewToken(tokenGenerator, ipAddress);
    }

    private TokenModel GenerateNewToken(IUserTokenGenerator tokenGenerator, string ipAddress)
    {
        var userInfo = new UserInfoModel(Id, FirstName, LastName, UserName, PhoneNumber.ToString(), Roles);
        var tokenModel = tokenGenerator.GenerateToken(userInfo, ipAddress);

        RefreshTokens.Add(RefreshToken.Create(tokenModel.RefreshToken, tokenModel.RefreshTokenExpires, tokenModel.IpAddress));

        return tokenModel;
    }

    public void SetPassword(IPasswordDomainService passwordService, string password)
    {
        CheckRule(new PasswordComplexityRule(password));

        Password = passwordService.Hash(password);
    }

    public void ChangePassword(IPasswordDomainService passwordService, string oldPassword, string newPassword)
    {
        CheckRule(new PasswordCompareRule(passwordService, oldPassword, Password));

        SetPassword(passwordService, newPassword);
    }

    public TokenModel RefreshNewToken(IUserTokenGenerator tokenGenerator,
        string token, string ipAddress)
    {
        CheckRule(new RefreshTokenCheckRule(RefreshTokens, token));

        var refreshToken = RefreshTokens.Single(t => t.Token.Equals(token));

        refreshToken.Revoke(ipAddress);

        return GenerateNewToken(tokenGenerator, ipAddress);
    }

    public void Active()
    {
        IsActivated = true;
    }
    public void Deactivate()
    {
        IsActivated = false;
    }
}