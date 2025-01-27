using Dimadev.Core.Handlers;
using Dimadev.Core.Models.Reports;
using Dimadev.Core.Requests.Reports;
using Dimadev.Core.Responses;
using System.Net.Http.Json;

namespace Dimadev.Web.Handlers
{
    public class ReportHandler (IHttpClientFactory httpClientFactory) : IReportHandler

    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync (GetIncomesAndExpensesRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<IncomesAndExpenses>?>>
                ($"/incomes-expenses")
                ?? new Response<List<IncomesAndExpenses>?>(null,400, "Nao foi possível obter os dados.");
        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<IncomesByCategory>?>>
                ($"/incomes")
                ?? new Response<List<IncomesByCategory>?>(null, 400, "Nao foi possível obter os dados.");
        }
       
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<ExpensesByCategory>?>>
                ($"/reports/expenses")
         
                ?? new Response<List<ExpensesByCategory>?>(null, 400, "Nao foi possível obter os dados.");
        }

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            return await _client.GetFromJsonAsync<Response<FinancialSummary?>>
                ($"/summary")
                ?? new Response<FinancialSummary?>(null, 400, "Nao foi possível obter os dados.");
        }
    }
}
