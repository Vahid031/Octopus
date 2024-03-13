using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Presentation.Http.Configurations.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Octopus.UserManagement.Presentation.Http.Users.Services;

internal class JwtUserTokenGenerator : IUserTokenGenerator
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtUserTokenGenerator(
        IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public TokenModel GenerateToken(UserInfoModel user, string ipAddress)
    {
        var roleClaims = new List<Claim>();


        for (int i = 0; i < user.Roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", user.Roles[i].ToString()));
        }

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim("uid", user.Id.ToString()),
                new Claim("ip", ipAddress)
            }
            .Union(roleClaims);

        var expireIn = DateTime.UtcNow.Add(_jwtOptions.Value.TokenDuration);

        return GenerateSignInModel(expireIn, ipAddress, claims.ToArray());
    }

    private TokenModel GenerateSignInModel(DateTime expires, string ipAddress, Claim[] claims)
    {
        JwtSecurityToken jwtSecurityToken = GenerateJsonWebToken(expires, claims);

        return new TokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            TokenType = JwtBearerDefaults.AuthenticationScheme,
            RefreshToken = RandomTokenString(),
            RefreshTokenExpires = DateTimeOffset.UtcNow.Add(_jwtOptions.Value.RefreshTokenDuration),
            IpAddress = ipAddress,
            AccessTokenExpires = expires
        };
    }

    private JwtSecurityToken GenerateJsonWebToken(DateTime expires, Claim[] claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: claims,
            expires: expires,
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
}