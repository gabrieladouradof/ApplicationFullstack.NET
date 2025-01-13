using Dimadev.Api.Data;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dimadev.Api.Handlers;

public class VoucherHandler(AppDbContext context) : IVoucherHandler
{
    public async Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request)
    {
        try
        {
            var voucher = await context
                .Vouchers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => 
                x.Number == request.Number &&
                x.IsActive == true);

            return voucher is null
                ? new Response<Voucher?>(null, 404, "Voucher nao encontrado :(")
                : new Response<Voucher?>(voucher);
        }
        catch
        {
            return new Response<Voucher?>(null, 500, "Nao foi possível recuperar seu voucher");
        }
    }
}


