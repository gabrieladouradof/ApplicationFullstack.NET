using Dimadev.Api.Data;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dimadev.Api.Handlers
{
    public class ProductHandler(AppDbContext context) : IProductHandler
    {
        public async Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request)
        {
            try
            {
                var query = context
                    .Products
                    .AsNoTracking()
                    .Where(x => x.IsActive == true)
                    .OrderBy(x => x.Title);

                var produtos = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Product>?>
                    (
                    produtos,
                    count,
                    request.PageNumber,
                    request.PageSize);

            }
            catch
            {
                return new PagedResponse<List<Product>?>(null, 500, "Nao foi possivel consultar os produtos.");
            }
        }
        public async Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request)
        {
            try
            {
                var product = await context
                    .Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Slug == request.Slug && x.IsActive == true
                    );

                return product is null
                    ? new Response<Product?>(null, 404, "Produto nao encontrado")
                    : new Response<Product?>(product);
            }
            catch
            {
                return new Response<Product?>(null, 500, "Nao foi possivel recuperar seus produtos");
            }
        }

    }
}
