using Octopus.UserManagement.Core.Contract.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Services;

public interface IAuthenticationManager
{
    Task SignOut();
    Task<SignInModel> SignIn(SignInUserModel request);
}