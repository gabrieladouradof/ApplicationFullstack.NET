using Dimadev.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Recupera uma transacao")
            .WithDescription("Recupera uma transacao")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync
            (ClaimsPrincipal user,
            ITransactionHandler handler,
            long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}
