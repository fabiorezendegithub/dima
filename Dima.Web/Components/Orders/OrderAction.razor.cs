using Dima.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Dima.Web.Components.Orders;

public partial class OrderActionComponent : ComponentBase
{
    #region Parameters
    
    [Parameter, EditorRequired]
    public Order Order { get; set; } = null!;

    #endregion

}