using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dimadev.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Models;
using Dimadev.Api.Data;
using Dima.Core.Requests.Transactions;
using Dimadev.Core.Common.Extensions;

namespace Dima.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction>(transaction, 201, "Transacao criada com sucesso.");
            }
            catch (Exception ex) 
            {
                return new Response<Transaction>(null, 500, $"Erro ao criar a transacao: {ex.Message}");
            }
        }
        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context
                    .Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transacao nao encontrada");

            //    transaction.PairdOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel recuperacao sua transacao");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = await context
                .Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transacao nao encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel recuperar sua transacao");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync (GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Transacao nao encontrada")
                    : new Response<Transaction?>(transaction);

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possível recuperar sua transacao");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel determinar a data de início ou término");
            }

            try
            {
                var query = context.Transactions.AsNoTracking().Where(x =>

                x.CreatedAt >= request.StartDate &&
                x.CreatedAt <= request.EndDate &&
                x.UserId == request.UserId)
                    .OrderBy(x => x.CreatedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    count,
                    request.PageNumber,
                    request.PageSize
                    );
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel determinar a data de início e de término");
            }
            }
        }

    }

