using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dimadev.Core.Common.Extensions;
using Dimadev.Core.Responses;
using System.Net.Http.Json;
using System.Net.Http;
using Dima.Core.Models;


namespace Dimadev.Web.Handlers
{
    public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/transactions", request);
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                   ?? new Response<Transaction?>(null, 400, "Não foi possível criar sua transação");
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                   ?? new Response<Transaction?>(null, 400, "Não foi possível atualizar sua transação");
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await _client.DeleteAsync($"v1/transactions/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Transaction?>>()
                   ?? new Response<Transaction?>(null, 400, "Não foi possível excluir sua transação");
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Transaction?>>($"v1/transactions/{request.Id}")
               ?? new Response<Transaction?>(null, 400, "Não foi possível obter a transação");

        //O datetime no .NET sao structs. Isso implica que devemos modificar o GetTransactions para que passe dados corretos. Nao os padroes do Datetime.
        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";
            var startDate = request.StartDate is not null
                ? request.StartDate.Value.ToString(format)
                : DateTime.Now.GetFirstDay().ToString(format);

            var endDate = request.EndDate is not null
                ? request.EndDate.Value.ToString(format)
                : DateTime.Now.GetLastDay().ToString(format);

            var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";

            return await _client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url)
                ?? new PagedResponse<List<Transaction>?>(null, 400, "Não foi possível obter as transações");
        }
    }
}
