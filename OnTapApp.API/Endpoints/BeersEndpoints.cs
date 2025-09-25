using OnTapApp.API.Infrastructure.Contracts;

namespace OnTapApp.API.Endpoints;

public static class BeersEndpoints
{
    public static WebApplication UseBeersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/beers");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            return await unitOfWork.Beers.ListAsync();
            
        }).WithName("GetAllBeers");
        
        group.MapGet("/styles", async (IUnitOfWork unitOfWork) =>
            {
            
                return await unitOfWork.Beers.GetBeerStyles();
            })
            .WithName("GetBeerStyles");

        group.MapGet("/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
        {
            return await unitOfWork.Beers.GetByIdAsync(id);

        }).WithName("GetBeerById");
        
        return app;
    }
}