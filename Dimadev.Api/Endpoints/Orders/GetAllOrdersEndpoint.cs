using Dimadev.Api.Common.Api;
using Dimadev.Core;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Orders;

    public class GetAllOrdersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Orders: Get All")
            .WithSummary("Recupera todos os pedidos.")
            .WithDescription("Recupera todos os pedidos.")
            .WithOrder(5)
            .Produces<PagedResponse<List<Order>?>> ();
        

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IOrderHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
            {
                var request = new GetAllOrdersRequest
                {
                    UserId = user.Identity!.Name ?? string.Empty,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await handler.GetAllAsync(request);
                return result.IsSucess
                    ? TypedResults.Ok(result)
                    : TypedResults.BadRequest(result);
            }
        }

    
