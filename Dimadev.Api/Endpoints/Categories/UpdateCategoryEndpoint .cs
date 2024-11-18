using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dimadev.Api.Common.Api;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories Updatee")
            .WithSummary("Atualiza uma nova categoria")
            .WithDescription(":)")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        public static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            UpdateCategoryRequest request,
            long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSucess
               ? TypedResults.Ok(result)
               : Results.BadRequest(result);

        }
    }
}
