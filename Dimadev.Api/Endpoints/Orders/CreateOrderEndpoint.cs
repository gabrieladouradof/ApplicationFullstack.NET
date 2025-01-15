using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Orders;

public class CreateOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/{id}/cancel", HandleAsync)
        .WithName("Orders: Create a new order")
        .WithSummary("Cria um novo pedido.");
        
    private static async Task<IResult> HandleAsync(IOrderHandler handler, 
        CreateOrderRequest request, 
        ClaimsPrincipal user)
    {
        request.UserId = user.Identity!.Name ?? string.Empty;

        var result = await handler.CreateAsync(request);
        return result.IsSucess
            ? TypedResults.Created($"v1/orders/{result.Data.Number}", result)
            : TypedResults.BadRequest(result);
            
    }
}
