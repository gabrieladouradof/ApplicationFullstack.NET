using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;

namespace Dimadev.Web.Pages.Categories
{
    public partial class EditCategoryPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; }
        public UpdateCategoryRequest InputModel { get; set; } = new();

        [Parameter]
        public string Id { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            var request = new GetCategoryByIdRequest
            {
                Id = long.Parse(Id)
            };
            var response = await Handler.GetByIdAsync(request);
            if (response is { IsSucess: true, Data: not null })
                InputModel = new UpdateCategoryRequest
                {
                    Id = response.Data.Id,
                    Title = response.Data.Title,
                    Description = response.Data.Description
                };

        }
        #endregion

    }
}
