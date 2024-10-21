﻿using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Orders;

public partial class CheckoutPage : ComponentBase
{
    #region Parameters
    [Parameter]
    public string ProductSlug { get; set; } = string.Empty;
    [SupplyParameterFromQuery(Name ="voucher")]
    public string? VoucherNumber { get; set; }

    #endregion
    #region Properties
    public bool IsBusy { get; set; } = false;
    public bool IsValid { get; set; } = false;
    public CreateOrderRequest InputModel { get; set; } = new();
    public Product? Product { get; set; }
    public Voucher? Voucher { get; set; }
    public decimal Total { get; set; }
    #endregion

    #region Services
    [Inject]
    public IProductHandler ProductHandler { get; set; } = null!;
    [Inject]
    public IOrderHandler OrderHandler { get; set; } = null!;
    [Inject]
    public IVoucherHandler VoucherHandler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region overrides
    protected override async Task OnInitializedAsync()
    {                      
        try
        {
            var result = await ProductHandler.GetBySlugAsync(new GetProductBySlugRequest 
            {  
                Slug = ProductSlug 
            });

            if (result.IsSuccess == false)
            {
                Snackbar.Add("Não foi possível obter o produto", Severity.Error);
                IsValid = false;
                return;
            }

            Product = result.Data;
        }
        catch
        {
            Snackbar.Add("Não foi possível obter o produto", Severity.Error);
            IsValid = false;
        }

        if (Product is null)
        {
            Snackbar.Add("Não foi possível obter o produto", Severity.Error);
            IsValid = false;
            return;
        }

        if(string.IsNullOrEmpty(VoucherNumber) == false)
        {
            try
            {
                var result = await VoucherHandler.GetByNumberAsync(new GetVoucherBynumberRequest
                {
                    Number = VoucherNumber.Replace("-", "")
                });

                if (result.IsSuccess is false)
                {
                    VoucherNumber = string.Empty;
                    Snackbar.Add("Não foi possível obter o voucher");
                }

                if (result.Data is null)
                {
                    VoucherNumber = string.Empty;
                    Snackbar.Add("Não foi possível obter o voucher");
                }

                Voucher = result.Data;
            }
            catch
            {
                VoucherNumber = string.Empty;
                Snackbar.Add("Não foi possível obter o voucher");
            }
        }

        IsValid = true;
        Total = Product.Price - (Voucher?.Amount ?? 0);
    }
    #endregion

    #region Methods
    //public async Task OnValidSubmitAsync()
    //{
    //    IsBusy = true;

    //    try
    //    {
    //        var result = await Handler.CreateAsync(InputModel);
    //        if (result.IsSuccess)
    //        {
    //            Snackbar.Add(result.Message ?? "Categoria criada com sucesso", Severity.Success);
    //            NavigationManager.NavigateTo("/categorias");
    //        }
    //        else
    //        {
    //            Snackbar.Add(result.Message ?? "Erro ao criar categoria", Severity.Error);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Snackbar.Add(ex.Message, Severity.Error);
    //    }
    //    finally
    //    {
    //        IsBusy = false;
    //    }
    //}
    #endregion
}