using Microsoft.AspNetCore.Components;
using MudBlazor;
using Dimadev.Web.Handlers;
using Dimadev.Core.Handlers;


namespace Dimadev.Web.Pages
{
    public partial class HomePage : ComponentBase
    {

        #region Properties
        #endregion

        #region Services
        
        [Inject]
        public ISnackbar? Snackbar { get; set; }
        public IReportHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides
        #endregion

        #region Methods
        #endregion

    }
}
