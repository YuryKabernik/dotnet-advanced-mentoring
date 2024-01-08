using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.WebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public class CatalogService(IHttpClientFactory httpClientFactory) : ICatalogService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Item?> GetItem(string id, CancellationToken cancellation)
        {
            using var client = this._httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("https://localhost:8888/");

            var result = await client.GetAsync("/", cancellation);
            var item = await result.Content.ReadFromJsonAsync<Item>();

            return item;
        }
    }
}
