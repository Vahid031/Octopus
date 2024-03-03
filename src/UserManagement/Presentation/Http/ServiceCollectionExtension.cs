using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Presentation.Http.Configurations.Options;
using Octopus.UserManagement.Presentation.Http.Users.Services;

namespace Octopus.UserManagement.Presentation.Http;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementHttpServices(this IServiceCollection services, 
        IConfiguration configuration)
    {

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);

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

        services.AddSingleton<IUserTokenGenerator, ApplicationUserTokenGenerator>();

        return services;
    }
}
