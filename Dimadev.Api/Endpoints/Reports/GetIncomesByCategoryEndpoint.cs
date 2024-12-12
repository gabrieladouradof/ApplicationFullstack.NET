using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models.Reports;
using Dimadev.Core.Requests.Reports;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Reports
{
    public class GetIncomesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/incomes", HandleAsync)
            .Produces<Response<List<IncomesByCategory>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler)
        {
            var request = new GetIncomesByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetIncomesByCategoryReportAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
