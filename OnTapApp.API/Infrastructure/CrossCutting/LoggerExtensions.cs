using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace OnTapApp.API.Infrastructure.CrossCutting;

public static class LoggerExtensions
{
    public static WebApplicationBuilder AddLoggingServices(this WebApplicationBuilder builder)
    {
        string elasticHost = builder.Configuration["ElasticSearch:Host"] ?? "http://ontap-elasticsearch:9200";

        builder.Host.ConfigureServices((ctx, services) =>
        {

            var loggerConfig = new LoggerConfiguration()                       
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticHost)))
                .WriteTo.Console();
            
            string minLevel = builder.Configuration["ElasticSearch:LogLevel"] ?? "Information";
            
            SetMinimumLogLevel(loggerConfig, minLevel);

            Log.Logger = loggerConfig.CreateLogger();
                
        });

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        return builder;
    }
    
    private static void SetMinimumLogLevel(LoggerConfiguration loggerConfig, string minLevel)
    {
        switch (minLevel)
        {
            case "Debug":
                loggerConfig.MinimumLevel.Debug();
                break;
            case "Information":
            case "Info":
                loggerConfig.MinimumLevel.Information();
                break;
            case "Warning":
                loggerConfig.MinimumLevel.Warning();    
                break;
            case "Error":
                loggerConfig.MinimumLevel.Error();
                break;
            case "Fatal":
                loggerConfig.MinimumLevel.Fatal();
                break;
            default:
                loggerConfig.MinimumLevel.Information();
                break;               
        }
    }
}