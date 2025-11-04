namespace OnTapApp.API.Infrastructure.CrossCutting;

public static class ResilienceExtensions
{

    public static WebApplicationBuilder AddResiliencePolicies(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

        });

        return builder;
    }
}