using Dimadev.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dimadev.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dimadev.Core;

namespace Dima.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint :IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
               .WithName("Categories: Get All")
               .WithSummary("Recupera todas as categorias")
               .WithDescription("Recupera todas as categorias")
               .WithOrder(5)
               .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            //Reports that it comes from Query
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSucess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }             
    }
}
