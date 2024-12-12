using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models.Reports;
using Dimadev.Core.Requests.Reports;
using Dimadev.Core.Responses;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Reports
{
    public class GetExpensesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/expenses", HandleAsync)
            .Produces<Response<List<ExpensesByCategory>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler)
        {
            var request = new GetExpensesByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetExpensesByCategoryReportAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}