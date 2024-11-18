using MudBlazor;
using Microsoft.AspNetCore.Components;
using Dimadev.Web.Security;
using Dimadev.Core.Handlers;

namespace Dimadev.Web.Pages.Identity
{
    public partial class LogoutPage : ComponentBase
    {
        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IAccountHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
            {
                await Handler.LogoutAsync();
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
            }

            await base.OnInitializedAsync();
        }

        #endregion
    }
}
