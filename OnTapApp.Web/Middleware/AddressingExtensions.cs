using Microsoft.AspNetCore.HttpOverrides;

namespace OnTapApp.Web.Middleware;

public static class AddressingExtensions
{
    public static WebApplication UseNginxForwardedHeaders(this WebApplication app)
    {
        
        var options = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            RequireHeaderSymmetry = false
        };

        options.KnownNetworks.Clear(); // obrigatório em Docker
        options.KnownProxies.Clear();  // obrigatório em Docker

        app.UseForwardedHeaders(options);

        return app;
        
    }
}