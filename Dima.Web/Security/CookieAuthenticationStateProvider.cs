using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Dima.Web.Security;

public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) : 
             AuthenticationStateProvider, 
             ICookieAuthenticationStateProvider
{
    private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
    private bool _isAuthenticated = false;
    public async Task<bool> CheckAuthenticationAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }

    public void NotifyAuthenticationStateChanged()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private async Task<User?> GetUser()
    {
        try
        {
            return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");
        }
        catch
        {
            return null;
        }
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email)
        };

        return claims;
    }
}
