﻿using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dimadev.Core.Common.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dimadev.Web.Pages.Transactions
{
    public partial class ListTransactionsPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<Transaction> Transactions { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public int[] Years { get; set; } =
   {
        DateTime.Now.Year,
        DateTime.Now.AddYears(-1).Year,
        DateTime.Now.AddYears(-2).Year,
        DateTime.Now.AddYears(-3).Year
    };

        #endregion

        #region Services
        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;
        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
            => await GetTransactionsAsync();

        #endregion

        #region Public Methods

        public async Task OnSearchAsync()
        {
            await GetTransactionsAsync();
            StateHasChanged();
        }
        public async void OnDeleteButtonClickedAsync (long id, string title)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENCAO",
                $"Ao prosseguir o lancamento {title} será excluído. Esta acao é irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar"
                );

            if(result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }

        public Func<Transaction, bool> Filter => transaction =>
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return true;

            return transaction.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                   || transaction.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
        };

        #endregion

        #region Private Methods
        private async Task GetTransactionsAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetTransactionByPeriodRequest
                {
                    StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                    EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                    PageNumber = 1,
                    PageSize = 1000
                };
            }
            catch
            {
                IsBusy = false;
            }
        }

        private async Task OnDeleteAsync (long id, string title)
        {
            IsBusy = true;

            try
            {
                var result = await Handler.DeleteAsync(new DeleteTransactionRequest { Id = id });

                if (result.IsSucess)
                {
                    Snackbar.Add($"Lancamento {title} removido!", Severity.Success);
                    Transactions.RemoveAll(x => x.Id == id);
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
