using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;

namespace Dimadev.Web.Pages.Orders
{
    public partial class CheckoutPage : ComponentBase
    {
        #region Parameters

        [Parameter] public string ProductSlug { get; set; } = string.Empty;
        [SupplyParameterFromQuery(Name = "voucher")] public string? VoucherNumber { get; set; }

        #endregion

        #region Properties
        public PatternMask Mask = new("####-####")
        {
            MaskChars = [new MaskChar('#', @"[0-9a-fA-F")],
            Placeholder = '_',
            CleanDelimiters = true,
            Transformation = AllUpperCase
        };
        public bool IsBusy { get; set; }
        public bool IsValid { get; set; }
        public CreateOrderRequest InputModel { get; set; } = new();
        public Product? Product { get; set; }
        public Voucher? Voucher { get; set; }
        public decimal Total { get; set; }
        #endregion

        #region Services
        [Inject] public IProductHandler ProductHandler { get; set; } = null!;
        [Inject] public IOrderHandler OrderHandler { get; set; } = null!;
        [Inject] public IVoucherHandler VoucherHandler { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            // Recuperar o Produto
            // Recuperar o Voucher (n tao importante)

            try
            {
                var result = await ProductHandler.GetBySlugAsync(new GetProductBySlugRequest
                {
                    Slug = ProductSlug
                });
                if (result.IsSucess == false)
                {
                    Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                    IsValid = false;
                    return;
                }
                Product = result.Data;

            }
            catch
            {
                Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                IsValid = false;
                return;

            }
            if (Product is null)
            {
                Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                IsValid = false;
                return;
            }
            if (string.IsNullOrEmpty(VoucherNumber) == false)
            {
                try
                {
                    var result = await VoucherHandler.GetByNumberAsync(new GetVoucherByNumberRequest
                    {
                        Number = VoucherNumber.Replace("-", "")

                    });

                    if (result.IsSucess == false)
                    {
                        VoucherNumber = string.Empty;
                        Snackbar.Add("Não foi possível obter o voucher");
                    }
                    if (result.Data is null)
                    {
                        VoucherNumber = string.Empty;
                        Snackbar.Add("Nao foi possivel obter o voucher");
                    }
                    Voucher = result.Data;
                }
                catch
                {
                    VoucherNumber = string.Empty;
                    Snackbar.Add("Nao foi possivel obter o voucher");
                }

                IsValid = true;
                Total = Product.Price - (Voucher?.Amount ?? 0);
            }
            #endregion

            private static char AllUpperCase(char c) => c.ToString().ToUpperInvariant()[0];
        }
    }
}
