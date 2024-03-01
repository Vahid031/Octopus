using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

internal class IdentityAuthenticationManager : IAuthenticationManager
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityAuthenticationManager(
        IOptions<JwtOptions> jwtOptions,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtOptions = jwtOptions;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SignInModel> SignIn(SignInUserModel user)
    {
        var roleClaims = new List<Claim>();

        for (int i = 0; i < user.Roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", user.Roles[i]));
        }

        string ipAddress = GetHostIpAddress();
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim("uid", user.UserId),
                new Claim("ip", ipAddress)
            }
        .Union(roleClaims);

        var expireIn = DateTime.UtcNow.Add(_jwtOptions.Value.TokenDuration);

        var userIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, "", "");

        //await _httpContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme,
        //     new ClaimsPrincipal(userIdentity),
        //     new AuthenticationProperties
        //     {
        //         ExpiresUtc = expireIn,
        //         IsPersistent = true,
        //         AllowRefresh = true
        //     });

        return GenerateSignInModel(expireIn, claims.ToArray());
    }
    private SignInModel GenerateSignInModel(DateTime expireIn, Claim[] claims)
    {
        JwtSecurityToken jwtSecurityToken = GenerateJsonWebToken(expireIn, claims);

        return new SignInModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            TokenType = JwtBearerDefaults.AuthenticationScheme,
            ExpireIn = expireIn.Ticks,
            RefreshToken = RandomTokenString(),
        };
    }

    private JwtSecurityToken GenerateJsonWebToken(DateTime expireIn, Claim[] claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwtOptions.Value.Issuer,
        audience: _jwtOptions.Value.Audience,
        claims: claims,
        expires: expireIn,
        signingCredentials: signingCredentials);
        return jwtSecurityToken;
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

    public async Task SignOut()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(
            JwtBearerDefaults.AuthenticationScheme);
    }
}