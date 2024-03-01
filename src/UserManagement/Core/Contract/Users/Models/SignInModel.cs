namespace Octopus.UserManagement.Core.Contract.Users.Models;

public record SignInModel
{
    public string Id { get; init; }
    public string UserName { get; init; }
    public string PhoneNumber { get; init; }
    public List<string> Roles { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
