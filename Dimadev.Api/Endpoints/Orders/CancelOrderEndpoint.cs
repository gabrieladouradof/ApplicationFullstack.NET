using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Orders
{
    public class CancelOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id}/cancel", HandleAsync)
            .WithName("Orders: Cancel order")
            .WithSummary("Cancela um pedido.")
            .WithDescription("Cancela um pedido")
            .WithOrder(2)
            .Produces<Response<Order?>>();
            
        private static async Task<IResult> HandleAsync(IOrderHandler handler, long id, ClaimsPrincipal user)
        {
            var request = new CancelOrderRequest
            {
                Id = id,
                UserId = user.Identity!.Name ?? string.Empty
            };

            var result = await handler.CancelAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }

    }
}
