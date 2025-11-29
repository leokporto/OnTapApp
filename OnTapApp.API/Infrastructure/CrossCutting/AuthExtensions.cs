using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OnTapApp.API.Infrastructure.CrossCutting;

public static class AuthExtensions
{
    public static WebApplicationBuilder AddAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://accounts.google.com";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://accounts.google.com",
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Google:ClientId"],
                    ValidateLifetime = true
                };
            });

        builder.Services.AddAuthorization();

        return builder;

    }

    public static WebApplication UseAuthServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}