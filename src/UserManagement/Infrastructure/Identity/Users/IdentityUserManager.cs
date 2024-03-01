using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Models;
using Octopus.UserManagement.Core.Contract.Users.Services;
using Octopus.UserManagement.Core.Identity.Users.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Octopus.UserManagement.Core.Identity.Users;

internal class IdentityUserManager : IUserManager
{
    private readonly UserManager<OctopusIdentityUser> _userManager;
    private readonly SignInManager<OctopusIdentityUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IOptions<JwtOptions> _jwtOptions;


    public IdentityUserManager(UserManager<OctopusIdentityUser> userManager,
        IMapper mapper,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtOptions = jwtOptions;
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

        throw new OctopusException("");
    }

    public Task Confirm(string userId)
    {
        throw new NotImplementedException();
    }

    public Task SignOut(string userId) => _signInManager.SignOutAsync();

    public async Task<SignInModel> SignInWithPassword(SignInWithPasswordCommand request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            throw new ApplicationException($"No Accounts Registered with {request.UserName}.");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new ApplicationException($"Invalid Credentials for '{request.UserName}'.");
        }

        return await GenerateSignInModel(user, request.IpAddress);
    }

    public Task ChangePassword(ChangePasswordCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateOtpCode(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<SignInModel> SignInWithOtp(SignInWithOtpCommand request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            throw new ApplicationException($"No Accounts Registered with {request.UserName}.");
        }

        if(user.IsValidCode(_jwtOptions, request.Code))
        {
            throw new ApplicationException($"No Accounts Registered with {request.UserName}.");
        }

        await _signInManager.SignInAsync(user, isPersistent: true);

        return await GenerateSignInModel(user, request.IpAddress);
    }

    private async Task<SignInModel> GenerateSignInModel(OctopusIdentityUser user, string ipAddress)
    {
        JwtSecurityToken jwtSecurityToken = await GenerateJsonWebToken(user);
        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

        return new SignInModel
        {
            Id = user.Id,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Roles = rolesList.ToList(),
            RefreshToken = GenerateRefreshToken(ipAddress).Token,

        };
    }

    private async Task<JwtSecurityToken> GenerateJsonWebToken(OctopusIdentityUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        string ipAddress = GetHostIpAddress();

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwtOptions.Value.Issuer,
        audience: _jwtOptions.Value.Audience,
        claims: claims,
        expires: DateTime.UtcNow.Add(_jwtOptions.Value.TokenDuration),
        signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            Created = DateTimeOffset.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    private string RandomTokenString()
    {
        using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
        var randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    private string GetHostIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return string.Empty;
    }
}