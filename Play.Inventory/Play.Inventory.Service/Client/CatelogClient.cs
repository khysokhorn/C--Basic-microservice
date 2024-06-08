using ModelInventory;

namespace Inventor
{
    public class CatelogClient
    {
        private readonly HttpClient httpClient;
        public CatelogClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CatelogItemsDto>> GetcatelogItemsAsync()
        {
            var items = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatelogItemsDto>>("/movies/items");
            return items;
        }

    }
}