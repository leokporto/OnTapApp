using OnTapApp.Core.Entity;

namespace OnTapApp.Web.Services
{
    internal sealed class BeerService(HttpClient httpClient, ILogger<BeerService> logger)
    {
        public async Task<List<Beer>> GetAllBeersAsync()
        {
            List<Beer>? beers = null;

            try
            {
                var response = await httpClient.GetAsync("/beers");

                var responseText = await response.Content.ReadAsStringAsync();

                logger.LogInformation("Http status code: {StatusCode}", response.StatusCode);
                logger.LogInformation("Http response content: {ResponseText}", responseText);

                if(response.IsSuccessStatusCode)
                {
                    beers = await response.Content.ReadFromJsonAsync<List<Beer>>();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching beers from API");
            }

            return beers ?? new List<Beer>();
        }
    }
    
}
