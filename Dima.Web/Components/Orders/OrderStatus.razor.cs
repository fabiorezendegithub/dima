using Dima.Core.Enum;
using Microsoft.AspNetCore.Components;

namespace Dima.Web.Components.Orders;

public class OrderStatusComponent : ComponentBase
{
    #region Parameters
    [Parameter, EditorRequired]
    public EOrderStatus Status { get; set; }

    #endregion
    
}