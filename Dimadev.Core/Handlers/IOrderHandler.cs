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
    public interface IOrderHandler
    {
        Task<Response<Order?>> CancelAsync(CancelOrderRequest request);
        Task<Response<Order?>> CreateAsync(CreateOrderRequest request);
        Task<Response<Order?>> PayAsync(PayOrderRequest request);
        Task<Response<Order?>> RefundAsync(RefundOrderRequest request);
        Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrdersRequest request);
        Task<Response<Order?>> GetByNumber(GetOrderByNumberRequest request);

    }
}
