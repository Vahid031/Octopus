using System.Security.Cryptography;
using System.Text;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IPasswordDomainService
{
    string Hash(string password, out string passwordSalt);
    bool Equal(string password, string passwordHash, string passwordSalt);
}

internal class Sha256PasswordDomainService : IPasswordDomainService
{
    private const int Iterations = 350000;
    private const int KeySize = 16;
    private const int PasswordSize = 32;
    public string Hash(string password, out string passwordSalt)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        passwordSalt = Convert.ToHexString(salt);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            PasswordSize);

        return Convert.ToBase64String(hash);
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