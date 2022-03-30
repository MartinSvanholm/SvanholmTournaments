using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;
using SvanholmTournaments.Shared.Exeptions;

namespace SvanholmTournaments.Client.Services.AuthenticationServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticatedUserDTO> Login(UserDTO userDto)
    {
        var authResult = await _client.PostAsJsonAsync("api/Auth/login", userDto);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode == false) {
            throw new RequestErrorExeption(authResult.StatusCode, authContent);
        }

        var result = JsonSerializer.Deserialize<AuthenticatedUserDTO>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync("authToken", result.AccessToken);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.AccessToken);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);

        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}