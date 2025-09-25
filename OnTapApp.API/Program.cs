
using OnTapApp.API.Endpoints;
using OnTapApp.API.Infrastructure;
using OnTapApp.API.Infrastructure.Contracts;

namespace OnTapApp.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration.GetConnectionString("BeersDbConnection")!;
        
        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));
        builder.Services.AddScoped<IBeerRepository>(provider => provider.GetService<IUnitOfWork>()!.Beers);  // Opcional, para injeção direta
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseBeersEndpoints();

        app.Run();
    }
}
