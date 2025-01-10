using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Handlers
{
    public interface IProductHandler
    {
        Task<PagedResponse<List<Product>?>> GetAllAsync (GetAllProductsRequest request);
        Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request); 
    }
}
