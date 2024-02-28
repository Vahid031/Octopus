using AutoMapper;
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
    private readonly IMapper _mapper;

    public IdentityUserManager(UserManager<OctopusIdentityUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ApplicationUserModel> FindByUserName(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return null;

        return _mapper.Map<ApplicationUserModel>(user);
    }

    public async Task<ApplicationUserModel> FindById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return null;

        return _mapper.Map<ApplicationUserModel>(user);
    }

    public async Task Create(RegisterCommand request)
    {
        var userModel = _mapper.Map<OctopusIdentityUser>(request);

        var result = await _userManager.CreateAsync(userModel, request.Password);

        if (result.Succeeded) return;

        throw new Exception()
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