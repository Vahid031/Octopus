using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.ValueObjects;

public class Password : ValueObject<Password>
{
	public string PasswordHash { get; private set; }
	public string PasswordSalt { get; private set; }

	private Password(string passwordHash, string passwordSalt)
	{
		if (string.IsNullOrWhiteSpace(passwordHash) || string.IsNullOrWhiteSpace(passwordSalt))
			throw new UserPasswordInvalidFormatException();

		PasswordHash = passwordHash;
		PasswordSalt = passwordSalt;
	}

	private static Password ToPassword(string passwordSalt, string passwordHash) => new Password(passwordSalt, passwordHash);
	public static (string, string) FromPassword(Password password) => (password.PasswordSalt, password.PasswordHash);

	public static implicit operator Password((string passwordSalt, string passwordHash) password) =>
		ToPassword(password.passwordSalt, password.passwordHash);

	public static explicit operator (string PasswordSalt, string PasswordHash)(Password password) =>
		FromPassword(password);

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return PasswordHash;
		yield return PasswordSalt;
	}
}