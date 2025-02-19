using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Dimadev.Web.Pages.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.Design;

namespace Dimadev.Web.Components.Orders
{
    public partial class OrderActionComponent : ComponentBase
    {
        #region Parameters

        [CascadingParameter]
        public DetailsPage Parent { get; set; } = null!;

        [Parameter, EditorRequired]
        public Order Order { get; set; } = null!;
        #endregion

        #region Services
        [Inject] public IDialogService DialogService { get; set; } = null!;
        [Inject] public IOrderHandler OrderHandler { get; set; } = null!;
        [Inject] public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Public Methods
        public async void OnCancelButtonClicked ()
        {
            bool? result = await DialogService.ShowMessageBox("ATENÇAO", "Deseja realmente cancelar esse pedido?",
                yesText:"SIM", cancelText:"NAO");

            if (result is not null && result == true)
            {
                await CancelOrderAsync();
            }
        }

        public async void OnPayButtonClickedAsync()
        {
            await PayOrderAsync();
        }

        public async void OnRefundButtonClickedAsync()
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇAO",
                "Deseja realmente estornar esse pedido?",
                yesText: "SIM", cancelText: "NAO");

            if (result is not null && result == true)
                await RefundOrderAsync();
        }

        #endregion

        #region Private Methods
        private async Task CancelOrderAsync()
        {
            var request = new CancelOrderRequest
            {
                Id = Order.Id
            };

            var result = await OrderHandler.CancelAsync(request);
            if (result.IsSucess)
                Parent.RefreshState(result.Data!);

            else
                Snackbar.Add(result.Message, Severity.Error);
        }

        private async Task PayOrderAsync()
        {
            await Task.Delay(1);
            Snackbar.Add("Pagamento nao implementado.");
        }

        private async Task RefundOrderAsync()
        {

            var request = new RefundOrderRequest
            {
                Id = Order.Id
            };

            var result = await OrderHandler.RefundAsync(request);

            if (result.IsSucess)
                Parent.RefreshState(result.Data!);
            else Snackbar.Add(result.Message, Severity.Error);
        }
        #endregion
    }
}
