using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;


namespace OnTapApp.Web.Internal.Auth;

public static class AuthExtensions
{
    public static WebApplicationBuilder AddAuthServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = "https://accounts.google.com";
                options.ClientId = builder.Configuration["Google:ClientId"];
                options.ClientSecret = builder.Configuration["Google:ClientSecret"];
                options.ResponseType = "code";
                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");

                options.GetClaimsFromUserInfoEndpoint = true;
                
                options.CallbackPath = "/signin-oidc";
                
                
            });

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        return builder;
    }
    
    public static WebApplication UseAuthServices(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}