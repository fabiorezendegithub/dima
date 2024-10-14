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

    #region Public Methods

    public async Task OnSearchAsync()
    {
        await GetTransactionsAsync();
        StateHasChanged();
    }

    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir o lançamento {title} será excluído. Essa é uma ação irreversível! Deseja continuar?",
            yesText: "EXCLUIR",
            noText: "CANCELAR");

        if (result is true)
            await OnDeleteAsync(id, title);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteTransactionRequest { Id = id };
            var result = await Handler.DeleteAsync(request);
            if (result.IsSuccess)
            {
                Transactions.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Lançamento {title} excluído com sucesso.", Severity.Success);
            }
            else
            {
                Snackbar.Add(result.Message ?? $"Erro ao tentar excluir o lançamento {title}", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    public Func<Transaction, bool> Filter => transaction =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        return transaction.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
            || transaction.Title.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
    };



    #endregion
}
