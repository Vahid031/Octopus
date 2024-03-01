namespace Octopus.UserManagement.Core.Contract.Users.Models;

public record SignInModel
{
    public string TokenType { get; init; }
    public long ExpireIn { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
