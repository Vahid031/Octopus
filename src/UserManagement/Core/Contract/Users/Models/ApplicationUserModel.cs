namespace Octopus.UserManagement.Core.Contract.Users.Models;

public record ApplicationUserModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
}