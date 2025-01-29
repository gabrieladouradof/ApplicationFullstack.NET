using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Orders
{
    public class PayOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder App)
       => App.MapPost("/{id}/pay", HandleAsync)
       .WithName("Orders: Pay and order")
           .WithSummary("Paga um pedido")
           .WithDescription("Paga um pedido")
           .WithOrder(3)
           .Produces<Response<Voucher?>>();

        private static async Task<IResult> HandleAsync
            (IOrderHandler handler, long id, PayOrderRequest request, ClaimsPrincipal user)
        {
            request.Id = id;
            request.UserId = user.Identity!.Name ?? string.Empty;

            var result = await handler.PayAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);


        }
    }
}
