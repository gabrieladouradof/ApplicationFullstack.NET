using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Orders;

public class RefundOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder App)
      => App.MapPost("/{id}/refund", HandleAsync)
      .WithName("Orders: Refund an order")
          .WithSummary("Estorna um pedido")
          .WithDescription("Estorna um pedido")
          .WithOrder(6)
          .Produces<Response<Voucher?>>();

    private static async Task<IResult> HandleAsync 
        (IOrderHandler handler, long id, ClaimsPrincipal user)
{
        var request = new RefundOrderRequest
        {
            Id = id,
            UserId = user.Identity!.Name ?? string.Empty
        };
        var result = await handler.RefundAsync (request);
        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest (result);
}
}