﻿using Dima.Core.Enums;
using Dimadev.Api.Data;
using Dimadev.Core.Handlers;
using Dimadev.Core.Models.Reports;
using Dimadev.Core.Requests.Reports;
using Dimadev.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace Dimadev.Api.Handlers
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            await Task.Delay(1280); 
            try
            {
                var data = await context
                .IncomesAndExpenses
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

                return new Response<List<IncomesAndExpenses>?> (data);
            }
            catch 
            {
                return new Response<List<IncomesAndExpenses>?> (null, 500, "Não foi possível obter as entradas e saídas");
            }    
        }

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            try
            {
                var data = await context
                .IncomesByCategories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Category)
                .ToListAsync();

                return new Response<List<IncomesByCategory>?> (data);
            }
            catch
            {
                return new Response<List<IncomesByCategory>?>(null, 500, 
                    "Não foi possível obter as entradas por categorias");
            }

        }
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            try
            {
                var data = await context
                .ExpensesByCategories
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Category)
                .ToListAsync();

                return new Response<List<ExpensesByCategory>?>(data);   

            }
            catch
            {
                return new Response<List<ExpensesByCategory>?>
                    (null, 500, "Não foi possível obter as entradas por categorias");
            }
        }

        //classe para resumo financeiro, comeca do dia primeiro (1)
        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            //construcao das datas
            var startDate = new DateTime (DateTime.Now.Year, DateTime.Now.Month, 1);    
            try
            {
                var data = await context
                    .Transactions
                    .AsNoTracking()
                    .Where(
                    x => x.UserId == request.UserId
                    && x.PaidOrReceivedAt >= startDate
                    && x.PaidOrReceivedAt <= DateTime.Now
                    )
                    .GroupBy(x => 1)
                    .Select
                    (x => new FinancialSummary(
                        request.UserId,
                        //soma receitas e despesas 
                        x.Where(ty => ty.Type == ETransactionType.Deposit).Sum(t => t.Amount),
                        x.Where(ty => ty.Type == ETransactionType.Withdraw).Sum(t => t.Amount)
                    ))
                    .FirstOrDefaultAsync();

                return new Response<FinancialSummary?>(data);
            }
            catch
            {
                return new Response<FinancialSummary?>
                   (null, 500, "Não foi possível obter o resultado financeiro");
            }
        }
    }
}
