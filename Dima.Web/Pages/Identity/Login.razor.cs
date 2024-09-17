using Dima.Core.Handlers;
using Dima.Core.Requests.Accounts;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Dependences
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IAccountHandler Handler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    #endregion

    #region Properties
    public LoginRequest InputModel { get; set; } = new();
    public bool IsBusy { get; set; } = false;
    #endregion

    #region Override
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }
    #endregion

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message ?? "Login efetuado com sucesso", Severity.Success);
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add(result.Message ?? "Erro ao efetuar o login", Severity.Error);
            }
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
}
