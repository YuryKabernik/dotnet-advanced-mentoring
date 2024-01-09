using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.DataAccess.ValueObjects;
using CartingService.WebApi.Options;
using Microsoft.Extensions.Options;

namespace CartingService.WebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="options"></param>
    public class CatalogService(IHttpClientFactory httpClientFactory, IOptions<CatalogServiceOptions> options) : ICatalogService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly CatalogServiceOptions _options = options.Value;

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

            client.BaseAddress = new Uri(this._options.BaseAddress);

            var result = await client.GetAsync(this._options.Path, cancellation);
            var item = await result.Content.ReadFromJsonAsync<Item>(cancellation);

            return item;
        }
    }
}
