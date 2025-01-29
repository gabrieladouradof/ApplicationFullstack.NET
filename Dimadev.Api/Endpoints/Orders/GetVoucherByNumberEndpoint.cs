using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;

namespace Dimadev.Api.Endpoints.Orders
{
    public class GetVoucherByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder App)
        => App.MapGet("/{number}", HandleAsync)
        .WithName("Voucher: Get By Number")
            .WithSummary("Recupera um voucher")
            .WithDescription("Recupera um voucher")
            .WithOrder(1)
            .Produces<Response<Voucher?>>();

        private static async Task<IResult> HandleAsync
            (IVoucherHandler handler, string number)
        {
            var request = new GetVoucherByNumberRequest
            {
                Number = number
            };

            var result = await handler.GetByNumberAsync (request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
