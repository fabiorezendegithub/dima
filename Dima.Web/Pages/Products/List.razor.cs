using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Products;

public partial class ListProductsPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Product> Products { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public IProductHandler Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region overrides
    protected override async Task OnInitializedAsync()
        => await GetProductsAsync();

    #endregion

    #region Private Methods
    private async Task GetProductsAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllProductsRequest();

            var result = await Handler.GetAllAsync(request);

            if (result.IsSuccess)
                Products = result.Data ?? [];
            else
                Snackbar.Add(result.Message ?? string.Empty, Severity.Error);
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
