using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace Dimadev.Web.Handlers
{
    public class ProductHandler(IHttpClientFactory httpClientFactory) : IProductHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Product>?>>("v1/products")
        ?? new PagedResponse<List<Product>?>(null, 400, "Nao foi possível obter os produtos.");
        public async Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request)
        => await _client.GetFromJsonAsync<Response<Product?>>($"v1/products/{request.Slug}")
        ?? new Response<Product?>(null, 400, "Nao foi possível obter o produto");

    }
}
