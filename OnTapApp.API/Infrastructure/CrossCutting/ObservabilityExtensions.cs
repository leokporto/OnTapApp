using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OnTapApp.API.Infrastructure.CrossCutting;

public static class ObservabilityExtensions
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault()
            .AddService(serviceName: "SpinTrack.Web", serviceVersion: "1.0");
        builder.Services.AddOpenTelemetry()
            .WithTracing((traceBuilder) =>
            {
                traceBuilder
                    .AddSource("SpinTrack.Web")
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();


                traceBuilder.AddOtlpExporter(cfg =>
                {
                    string? endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
                    if (string.IsNullOrEmpty(endpoint))
                        endpoint = "http://ontap-apm-server:4317";

                    cfg.Endpoint = new Uri(endpoint);
                });

                traceBuilder.AddConsoleExporter();
            })
            .WithLogging();
        builder.Services.AddAllElasticApm();

        return builder;
    }
}