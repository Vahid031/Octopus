using Octopus.UserManagement.Core.Domain.Users.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IPasswordDomainService
{
	Password Hash(string password);
	bool Equal(string password, string passwordHash, string passwordSalt);
}

internal class Sha256PasswordDomainService : IPasswordDomainService
{
	private const int Iterations = 350000;
	private const int KeySize = 16;
	private const int PasswordSize = 32;
	public Password Hash(string password)
	{
		var salt = RandomNumberGenerator.GetBytes(KeySize);

		var hash = Rfc2898DeriveBytes.Pbkdf2(
			Encoding.UTF8.GetBytes(password),
			salt,
			Iterations,
			HashAlgorithmName.SHA256,
			PasswordSize);

		var passwordHash = Convert.ToBase64String(hash);
		var passwordSalt = Convert.ToHexString(salt);

		return (passwordHash, passwordSalt);
	}

	public bool Equal(string password, string passwordHash, string passwordSalt)
	{
		var salt = Convert.FromHexString(passwordSalt);

		var hash = Rfc2898DeriveBytes.Pbkdf2(
			Encoding.UTF8.GetBytes(password),
			salt,
			Iterations,
			HashAlgorithmName.SHA256,
			PasswordSize);

		return Convert.ToBase64String(hash).Equals(passwordHash);
	}
}