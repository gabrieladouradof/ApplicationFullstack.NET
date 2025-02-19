using Dimadev.Core.Enums;
using Microsoft.AspNetCore.Components;

namespace Dimadev.Web.Components.Orders
{
    public partial class OrderStatusComponent : ComponentBase
    {
        #region Parameters

        [Parameter, EditorRequired]
        public EOrderStatus Status { get; set; }

        #endregion
    }
}
