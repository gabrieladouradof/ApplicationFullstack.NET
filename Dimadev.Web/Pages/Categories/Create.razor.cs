using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dimadev.Web.Pages.Categories
{
    public class CreateCategoryPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public CreateCategoryPage InputModel { get; set; } = new();

        #endregion

        #region Services

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
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
