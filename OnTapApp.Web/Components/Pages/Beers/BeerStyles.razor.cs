
namespace OnTapApp.Web.Components.Pages.Beers;


public partial class BeerStyles
{
    public IEnumerable<string>? AllBeerStyles = null;

    
    protected override async Task OnInitializedAsync()
    {
        AllBeerStyles = await BeerService.GetAllBeerStylesAsync();
        await base.OnInitializedAsync();
    }
}