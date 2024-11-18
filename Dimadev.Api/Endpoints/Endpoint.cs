using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Transactions;
using Dimadev.Api.Common.Api;
using Dimadev.Api.Endpoints.Categories;
using Dimadev.Api.Endpoints.Identity;
using Dimadev.Api.Models;

namespace Dimadev.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints (this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");
                
            endpoints.MapGroup("/")
                .WithTags("Health check")
                .MapGet("/", () => new { message = "ÖK" });

            endpoints.MapGroup("v1/categories")
                .WithTags("Categories")
                //.RequireAuthorization ()
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                //.RequireAuthorization ()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetTransactionsByPeriodEndpoint>()
                .MapEndpoint<GetByIdEndpoint>();

            endpoints.MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            endpoints.MapGroup("v1/identity")
                .WithTags("Identities")
                .MapEndpoint<GetRolesEndpoint>()
                .MapEndpoint<LogoutEndpoint>();
                

        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint> (this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        { 
            TEndpoint.Map(app);
            return app;
        }
    }

}
