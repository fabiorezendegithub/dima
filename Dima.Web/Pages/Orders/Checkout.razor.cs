using Microsoft.AspNetCore.Components;

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
    //public bool IsBusy { get; set; } = false;
    //public CreateCategoryRequest InputModel { get; set; } = new();
    //#endregion

    //#region Services
    //[Inject]
    //public ICategoryHandler Handler { get; set; } = null!;
    //[Inject]
    //public NavigationManager NavigationManager { get; set; } = null!;
    //[Inject]
    //public ISnackbar Snackbar { get; set; } = null!;
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
