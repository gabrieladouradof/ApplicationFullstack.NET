using Dimadev.Api.Common.Api;
using Dimadev.Core;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dimadev.Api.Endpoints.Orders
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Products: Get All")
            .WithSummary("Recupera todos os produtos")
            .WithDescription("Recupera todos os produtos")
            .WithOrder(4)
            .Produces<PagedResponse<List<Product>?>>();
        private static async Task<IResult> HandleAsync(IProductHandler handler, 
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber, 
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllProductsRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
