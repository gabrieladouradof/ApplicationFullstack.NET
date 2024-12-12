using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models.Reports;
using Dimadev.Core.Requests.Reports;
using Dimadev.Core.Responses;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Reports
{
    public class GetFinancialSummaryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/summary", HandleAsync)
            .Produces<Response<List<ExpensesByCategory>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler)
        {
            var request = new GetFinancialSummaryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetFinancialSummaryReportAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}