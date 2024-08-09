﻿using Dima.Core.Handlers;
using Dima.Core.Requests.Accounts;
using Dima.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Dima.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

            return result.IsSuccessStatusCode
                ? new Response<string>("Login realizado com sucesso", 200, "Login realizado com sucesso")
                : new Response<string>(null, 400, "Usuário ou senha inválida");
        }

        public async Task<Response<string>> RegisterASync(RegisterRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/register", request);

            return result.IsSuccessStatusCode
                ? new Response<string>("Cadastro realizado com sucesso", 201, "Cadastro realizado com sucesso")
                : new Response<string>(null, 400, "Não foi possível realizar o cadastro");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
        }        
    }
}
