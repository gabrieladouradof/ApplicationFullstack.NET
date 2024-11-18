using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dimadev.Api.Common.Api;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dimadev.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories Create")
            .WithSummary("Cria uma nova categoria")
            .WithDescription(":)")
            .WithOrder(1)
            .Produces<Response<Category?>>();

        public static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            CreateCategoryRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            if (result.IsSucess)
                return Results.Created($"{result.Data?.Id}", result);

            return Results.BadRequest(result.Data);

        }
    }
}
