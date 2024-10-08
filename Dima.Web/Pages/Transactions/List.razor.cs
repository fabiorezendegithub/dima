﻿using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class ListTransactionsPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Transaction> Transactions { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    public int CurrentYear { get; set; } = DateTime.Now.Year;
    public int CurrentMonth { get; set; } = DateTime.Now.Month;
    public int[] Years { get; set; } =
    {
        DateTime.Now.Year,
        DateTime.Now.AddYears(-1).Year,
        DateTime.Now.AddYears(-2).Year,
        DateTime.Now.AddYears(-3).Year
    };
    #endregion

    #region Services
    [Inject]
    public ITransactionHandler Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;
    #endregion

    #region overrides
    protected override async Task OnInitializedAsync()
        => await GetTransactionsAsync();
    
    #endregion

    #region Private Methods
    private async Task GetTransactionsAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetTransactionsByPeriodRequest 
            {
                StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                PageNumber = 1,
                PageSize = 1000
            };

            var result = await Handler.GetByPeriodAsync(request);

            if (result.IsSuccess)
                Transactions = result.Data ?? [];
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
            if(result.IsSuccess)
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
