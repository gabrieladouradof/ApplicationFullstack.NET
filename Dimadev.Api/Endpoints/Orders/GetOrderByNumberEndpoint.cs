using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Orders
{
    public class GetOrderByNumberEndpoint : IEndpoint 
    {
       public static void Map(IEndpointRouteBuilder app)

            //parameter of string number
        => app.MapGet("/{number}", HandleAsync)
            .WithName("Orders: Get By Number")
            .WithSummary("Recupera um pedido pelo número")
            .WithDescription("Recupera um pedido pelo número")
            .WithOrder(6)
            .Produces<Response<Order>>();

        private static async Task<IResult> HandleAsync
            (ClaimsPrincipal user, IOrderHandler handler, string number)
        {
            var request = new GetOrderByNumberRequest
            {
                UserId = user.Identity!.Name ?? string.Empty,
                Number = number
            };

            var result = await handler.GetByNumberAsync(request);


            return result.IsSucess
               ? TypedResults.Ok(result)
              : TypedResults.BadRequest(result);
    }

    }
}
