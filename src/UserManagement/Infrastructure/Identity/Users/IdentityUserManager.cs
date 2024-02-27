using Microsoft.AspNetCore.Identity;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Models;
using Octopus.UserManagement.Core.Contract.Users.Services;
using Octopus.UserManagement.Core.Identity.Users.Models;

namespace Octopus.UserManagement.Core.Identity.Users;

internal class IdentityUserManager : IUserManager
{
    private readonly UserManager<OctopusIdentityUser> _userManager;

    public IdentityUserManager(UserManager<OctopusIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUserModel> FindByUserName(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return null;

        return 
    }

    public Task<ApplicationUserModel> FindById(string userId)
    {
        throw new NotImplementedException();
    }

    public Task Create(RegisterCommand request)
    {
        throw new NotImplementedException();
    }

    public Task Confirm(string userId)
    {
        throw new NotImplementedException();
    }

    public Task SignOut(string userId)
    {
        throw new NotImplementedException();
    }

    public Task SignIn(SignInWithPasswordCommand request)
    {
        throw new NotImplementedException();
    }

    public Task ChangePassword(ChangePasswordCommand request)
    {
        throw new NotImplementedException();
    }
}