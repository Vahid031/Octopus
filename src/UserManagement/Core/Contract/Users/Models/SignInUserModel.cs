namespace Octopus.UserManagement.Core.Contract.Users.Models;

public record SignInUserModel
{
    public string UserId { get; init; }
    public string PhoneNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public List<string> Roles { get; init; } = new();
}