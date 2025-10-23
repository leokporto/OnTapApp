using MudBlazor.Services;
using OnTapApp.Web.Components;
using OnTapApp.Web.Services;

namespace OnTapApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var ontapApiEndpoint = builder.Configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("OnTap App API is not set");
        

        builder.Services.AddSingleton<BeerService>();
        builder.Services.AddHttpClient<BeerService>(client =>
        {
            client.BaseAddress = new Uri(ontapApiEndpoint);
        });
        

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddMudServices();
        
        var app = builder.Build();        

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}