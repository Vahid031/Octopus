using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Services;

public interface IUserManager
{
    Task<ApplicationUserModel> FindByUserName(string username);
    Task<ApplicationUserModel> FindById(string userId);
    Task Create(RegisterCommand request);
    Task Confirm(string userId);
    Task SignOut(string userId);
    Task SignIn(SignInWithPasswordCommand request);
    Task ChangePassword(ChangePasswordCommand request);
}