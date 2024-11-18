using Dimadev.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dimadev.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Dimadev.Core;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get All")
            .WithSummary("Recupera todas as transacoes")
            .WithDescription("Recupera todas as transacoes")
            .WithOrder(5)
            .Produces<PagedResponse<Transaction?>>();

        private static async Task<IResult> HandleAsync
            (ClaimsPrincipal user,
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
            )
        {
            var request = new GetTransactionByPeriodRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate,

            };

            var result = await handler.GetByPeriodAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
