using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dimadev.Web.Pages
{
    public partial class HomePage : ComponentBase
    {

        #region Properties
        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; }
        public IReportHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides
        #endregion

        #region Methods
        #endregion

    }
}
