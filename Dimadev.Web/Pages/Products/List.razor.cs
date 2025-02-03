using Dimadev.Core.Handlers;
using Dimadev.Core.Models;
using Dimadev.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection.Metadata;

namespace Dimadev.Web.Pages.Products
{
    public partial class ListProductsPage : ComponentBase
    {

        #region Properties
        public bool IsBusy { get; set; }
        public List<Product> Products { get; set; } = [];

        #endregion

        #region Services
        [Inject] public ISnackbar Snackbar { get; set; } = null!;
        [Inject] public IProductHandler ProductHandler { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllProductsRequest();
                var result = await Handler.GetAllAsync(request);
                    
                if(result.IsSucess)
                {
                    Products = result.Data ?? [];
                }
                else
                    Snackbar.Add(result.Message, Severity.Error);
            }
            catch (Exception ex) 
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
            #endregion
        }
    }
}
