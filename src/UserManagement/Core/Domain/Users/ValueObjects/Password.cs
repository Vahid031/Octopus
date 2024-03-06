using Octopus.Core.Domain.Exceptions;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.ValueObjects;

public class Password : ValueObject<Password>
{
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }

    private Password(string passwordHash, string passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new OctopusValueObjectStateException("Password hash is invalid");

        if (string.IsNullOrWhiteSpace(passwordSalt))
            throw new OctopusValueObjectStateException("Password salt is invalid");

        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    private static Password ToPassword(string passwordSalt, string passwordHash) => new Password(passwordSalt, passwordHash);
    public static (string, string) FromPassword(Password password) => (password.PasswordSalt, password.PasswordHash);

    public static implicit operator Password((string passwordSalt, string passwordHash) password) =>
        ToPassword(password.passwordSalt, password.passwordHash);

    public static explicit operator (string PasswordSalt, string PasswordHash)(Password password) =>
        FromPassword(password);

    public override bool ObjectIsEqual(Password other)
    {
        return PasswordHash == other.PasswordHash && PasswordSalt == other.PasswordSalt;
    }

    public override int ObjectGetHashCode()
    {
        return HashCode.Combine(PasswordHash, PasswordSalt);
    }

    protected IEnumerable<object> GetEqualityComponents()
    {
        yield return PasswordHash;
        yield return PasswordSalt;
    }
}