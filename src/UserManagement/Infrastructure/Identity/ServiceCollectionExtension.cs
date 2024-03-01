using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Octopus.UserManagement.Core.Contract.Users.Services;
using Octopus.UserManagement.Core.Identity.Users;
using Octopus.UserManagement.Core.Identity.Users.Models;
using System.Text;

namespace Octopus.UserManagement.Core.Identity;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationManager, IdentityAuthenticationManager>();
        services.AddHttpContextAccessor();
        services.AddOptions<JwtOptions>()
          .Bind(configuration.GetSection(nameof(JwtOptions)));

        var jwtOptions = configuration
              .GetSection(nameof(JwtOptions))
              .Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            };
        });

        return services;
    }
}