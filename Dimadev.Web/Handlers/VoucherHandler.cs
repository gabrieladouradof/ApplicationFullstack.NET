using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Net.Http.Json;

namespace Dimadev.Web.Handlers
{
    public class VoucherHandler(IHttpClientFactory httpClientFactory): IVoucherHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request)
            => await _client.GetFromJsonAsync<Response<Voucher?>>($"v1/vouchers/{request.Number}")
                ?? new Response<Voucher?>(null, 400, "Nao foi possível abrir o voucher");
    }

}
