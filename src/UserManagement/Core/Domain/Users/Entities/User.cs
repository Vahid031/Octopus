using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Enums;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Rules;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
	#region Properties

	public string UserName { get; private set; }
	public Password Password { get; private set; }
	public PhoneNumber PhoneNumber { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public DateTimeOffset CreatedAt { get; private set; }
	public bool IsActivated { get; private set; }

	public List<OtpCode> OtpCodes { get; private set; }
	public List<RefreshToken> RefreshTokens { get; private set; }
	public List<RoleType> Roles { get; private set; }

	#endregion

	private User() { }

	private User(IUserRepository userRepository, string userName, PhoneNumber phoneNumber, string firstName, string lastName)
	{
		CheckRule(new UserNameMustBeAtLeast3CharacterRule(userName));
		CheckRule(new UserNameMustBeAtLast150CharacterRule(userName));
		CheckRule(new UserNameMustBeUniqueRule(userRepository, userName));

		CheckRule(new UserFirstNameMustBeAtLeast2CharacterRule(firstName));
		CheckRule(new UserFirstNameMustBeAtLast50CharacterRule(firstName));

		CheckRule(new UserLastNameMustBeAtLeast2CharacterRule(lastName));
		CheckRule(new UserLastNameMustBeAtLast50CharacterRule(lastName));

		UserName = userName;
		PhoneNumber = phoneNumber;
		FirstName = firstName;
		LastName = lastName;
		IsActivated = false;

		Roles = new List<RoleType>();
		OtpCodes = new List<OtpCode>();
		RefreshTokens = new List<RefreshToken>();
	}

	public static User Create(IUserRepository userRepository, string userName, string phoneNumber, string firstName, string lastName)
	{
		return new User(userRepository, userName, phoneNumber, firstName, lastName);
	}

	public bool OwnsToken(string token)
	{
		return RefreshTokens.Any(x => x.Token == token);
	}

	public string CreateNewOtpCode(string ipAddress, TimeSpan expireDuration)
	{
		var otpCode = OtpCode.Create(0, ipAddress, expireDuration);

		OtpCodes.Add(otpCode);

		return otpCode.Code;
	}

	private bool IsValidCode(string code)
	{
		if (OtpCodes is null || !OtpCodes.Any())
			return false;

		var otpCode = OtpCodes.LastOrDefault();

		if (otpCode == null || !otpCode.Code.Equals(code))
			return false;

		if (otpCode.IsExpired)
			return false;

		return true;
	}

	public TokenModel SignInWithPassword(IUserTokenGenerator tokenGenerator,
		IPasswordDomainService passwordService, string password, string ipAddress)
	{
		CheckRule(new UserPasswordMustBeEqualRule(passwordService, password, Password.PasswordHash, Password.PasswordSalt));

		return GenerateNewToken(tokenGenerator);
	}

	public TokenModel SignInWithOtp(IUserTokenGenerator tokenGenerator, string code, string ipAddress)
	{
		var isValidCode = IsValidCode(code);

		if (!isValidCode)
			throw new UserOtpCodeInvalidException(code);

		return GenerateNewToken(tokenGenerator);
	}

	private TokenModel GenerateNewToken(IUserTokenGenerator tokenGenerator)
	{
		var tokenModel = tokenGenerator.GenerateToken(new(Id, FirstName, LastName, UserName, PhoneNumber.ToString(), Roles));

		RefreshTokens.Add(RefreshToken.Create(tokenModel.RefreshToken, tokenModel.RefreshTokenExpires, DateTimeOffset.UtcNow, tokenModel.IpAddress));

		return tokenModel;
	}

	public void SetPassword(IPasswordDomainService passwordService, string password)
	{
		CheckRule(new UserPasswordMustBeAtLeast3CharacterRule(password));
		Password = passwordService.Hash(password);
	}

	public void ChangePassword(IPasswordDomainService passwordService, string oldPassword, string newPassword)
	{
		CheckRule(new UserPasswordMustBeEqualRule(passwordService, oldPassword, Password.PasswordHash, Password.PasswordSalt));

		SetPassword(passwordService, newPassword);
	}

	public void Active()
	{
		IsActivated = true;
	}
}