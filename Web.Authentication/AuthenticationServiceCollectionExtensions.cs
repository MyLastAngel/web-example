using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.Authentication.Models;
using Web.Authentication.Services;

namespace Web.Authentication;

public static class AuthenticationServiceCollectionExtensions
{
    public static void AddCustomAuthentication(this IServiceCollection services, AuthenticationOptions cfg)
    {
        services.AddSingleton((p) => Options.Create(cfg));

        services.AddScoped<ITokenGeneratorService, JwtTokenGeneratorService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = cfg.IsUseHttps;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = cfg.Issuer,

                    ValidateAudience = true,
                    ValidAudience = cfg.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = cfg.GetSymmetricKey(),
                    ValidateIssuerSigningKey = true
                };

            });
    }
}

