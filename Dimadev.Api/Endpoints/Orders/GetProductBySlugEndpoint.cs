using Dimadev.Api.Common.Api;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Orders;

public class GetProductBySlugEndpoint : IEndpoint
{
	public static void Map (IEndpointRouteBuilder App)
	=> App.MapGet("/{slug}", HandleAsync)
		.WithName("Products: Get By Slug")
			.WithSummary("Recupera um produto")
			.WithDescription("Recupera um produto")
			.WithOrder(2)
			.Produces<Response<Product?>>();

	private static async Task<IResult> HandleAsync(
		IProductHandler handler, string slug)
	{
		var request = new GetProductBySlugRequest
		{
			Slug = slug
		};

		var result = await handler.GetBySlugAsync(request);
		return result.IsSucess
			? TypedResults.Ok(result)
			: TypedResults.BadRequest(result);
	}
}