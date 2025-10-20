
using OnTapApp.Core.Entity;
using OnTapApp.Web.Services;

namespace OnTapApp.Web.Components.Pages
{
    public partial class Beers
    {
        public IEnumerable<Beer> AllBeers { get; set; } = new List<Beer>();

        protected override async Task OnInitializedAsync()
        {
            AllBeers = await beerService.GetAllBeersAsync();

            await base.OnInitializedAsync();
        }
    }
}
