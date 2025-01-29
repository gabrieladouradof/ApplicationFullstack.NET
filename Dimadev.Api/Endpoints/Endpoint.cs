using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Orders;
using Dima.Api.Endpoints.Transactions;
using Dimadev.Api.Common.Api;
using Dimadev.Api.Endpoints.Categories;
using Dimadev.Api.Endpoints.Identity;
using Dimadev.Api.Endpoints.Orders;
using Dimadev.Api.Endpoints.Reports;
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
                .MapEndpoint<CreateCategoryEndpoint>()
                .MapEndpoint<UpdateCategoryEndpoint>()
                .MapEndpoint<DeleteCategoryEndpoint>()
                .MapEndpoint<GetCategoryByIdEndpoint>()
                .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
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

            endpoints.MapGroup("v1/reports")
                .WithTags("Reports")
                .RequireAuthorization()
                .MapEndpoint<GetIncomesAndExpensesEndpoint>()
                .MapEndpoint<GetIncomesByCategoryEndpoint>()
                .MapEndpoint<GetExpensesByCategoryEndpoint>()
                .MapEndpoint<GetFinancialSummaryEndpoint>();

            endpoints.MapGroup("v1/products")
                .WithTags("Products")
                .RequireAuthorization()
                .MapEndpoint<GetAllProductsEndpoint>()
                .MapEndpoint<GetProductBySlugEndpoint>();

            endpoints.MapGroup("v1/vouchers")
                .WithTags("Vouchers")
                .RequireAuthorization()
                .MapEndpoint<GetVoucherByNumberEndpoint>();

            endpoints.MapGroup("v1/orders")
                .WithTags("Orders")
                .RequireAuthorization()
                .MapEndpoint<GetAllOrdersEndpoint>()
                .MapEndpoint<GetOrderByNumberEndpoint>()                
                .MapEndpoint<CreateOrderEndpoint>()
                .MapEndpoint<CancelOrderEndpoint>()
                .MapEndpoint<PayOrderEndpoint>()
                .MapEndpoint<RefundOrderEndpoint>();
        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint> (this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        { 
            TEndpoint.Map(app);
            return app;
        }
    }

}
