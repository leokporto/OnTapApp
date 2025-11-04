using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace OnTapApp.API.Infrastructure.CrossCutting;

public static class LoggerExtensions
{
    public static WebApplicationBuilder AddLoggingServices(this WebApplicationBuilder builder)
    {
        string elasticHost = builder.Configuration["ElasticSearch:Host"] ?? "http://ontap-elasticsearch:9200";

        var loggerConfig = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticHost))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"ontapapp-api-logs-{builder.Environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy.MM}",
                MinimumLogEventLevel = builder.Environment.IsDevelopment() ? Serilog.Events.LogEventLevel.Debug : Serilog.Events.LogEventLevel.Information
            })
            .WriteTo.Console();

        Log.Logger = loggerConfig.CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);

        return builder;
    }
}