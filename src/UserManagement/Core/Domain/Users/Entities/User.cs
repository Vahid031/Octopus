﻿using Octopus.Core.Domain.Entities;
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

    private List<OtpCode> _otpCodes;
    public IReadOnlyCollection<OtpCode> OtpCodes
    {
        get { return _otpCodes.AsReadOnly(); }
        private protected set { _otpCodes = value.ToList(); }
    }

    private List<RefreshTokenInfo> _refreshTokens;
    public IReadOnlyCollection<RefreshTokenInfo> RefreshTokens
    {
        get { return _refreshTokens.AsReadOnly(); }
        private protected set { _refreshTokens = value.ToList(); }
    }

    private List<RoleType> _roles;
    public IReadOnlyCollection<RoleType> Roles
    {
        get { return _roles.AsReadOnly(); }
        private protected set { _roles = value.ToList(); }
    }

    #endregion

    private User() { }

    private User(UserId id, string userName, PhoneNumber phoneNumber, string firstName, string lastName)
    {
        UserName = userName;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        IsActivated = false;
        CreatedAt = DateTimeOffset.UtcNow;
        Id = id;

        _roles = new List<RoleType>();
        _otpCodes = new List<OtpCode>();
        _refreshTokens = new List<RefreshTokenInfo>();
    }

    public static async Task<User> Create(IUserRepository userRepository, UserId id, string userName, PhoneNumber phoneNumber, string firstName, string lastName)
    {
        await CheckRuleAsync(new UserNameMustBeUniqueRule(userRepository, userName));
        await CheckRuleAsync(new PhoneNumberMustBeUniqueRule(userRepository, phoneNumber));
        CheckRule(new UserNameMustBeAtLeast3CharacterRule(userName));
        CheckRule(new UserNameMustBeAtLast150CharacterRule(userName));

        CheckRule(new UserFirstNameMustBeAtLeast2CharacterRule(firstName));
        CheckRule(new UserFirstNameMustBeAtLast50CharacterRule(firstName));

        CheckRule(new UserLastNameMustBeAtLeast2CharacterRule(lastName));
        CheckRule(new UserLastNameMustBeAtLast50CharacterRule(lastName));


        return new(id, userName, phoneNumber, firstName, lastName);
    }

    public OtpModel CreateNewOtpCode(IOtpConfiguration otpConfiguration, string ipAddress)
    {
        CheckRule(new SendOtpCodeRule(otpConfiguration, _otpCodes));

        var otpCode = OtpCode.Create(ipAddress);

        _otpCodes.Add(otpCode);

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

    public void TryOtp(string code)
    {
        _otpCodes.LastOrDefault()?.Retry();
    }

    public TokenModel SignInWithOtp(IUserTokenGenerator tokenGenerator,
    IOtpConfiguration otpConfiguration, string code, string ipAddress)
    {
        var otpCode = _otpCodes.LastOrDefault();

        CheckRule(new SignInWithOtpCodeRule(otpConfiguration, otpCode, code));

        otpCode!.Revoke(ipAddress);

        return GenerateNewToken(tokenGenerator, ipAddress);
    }

    private TokenModel GenerateNewToken(IUserTokenGenerator tokenGenerator, string ipAddress)
    {
        var userInfo = new UserInfoModel(Id, FirstName, LastName, UserName, PhoneNumber.ToString(), _roles);
        var tokenModel = tokenGenerator.GenerateToken(userInfo, ipAddress);

        _refreshTokens.Add(RefreshTokenInfo.Create(tokenModel.RefreshToken, tokenModel.RefreshTokenExpires, tokenModel.IpAddress));

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

    public TokenModel RefreshToken(IUserTokenGenerator tokenGenerator,
        string token, string ipAddress)
    {
        CheckRule(new RefreshTokenCheckRule(_refreshTokens, token));

        var refreshToken = _refreshTokens.Single(t => t.Token.Equals(token));

        refreshToken.Revoke(ipAddress);

        return GenerateNewToken(tokenGenerator, ipAddress);
    }

    public void AddRole(RoleType roleType)
    {
        if (_roles.Contains(roleType))
            return;

        _roles.Add(roleType);
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