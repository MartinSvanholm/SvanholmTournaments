using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Exeptions;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Client.Services.UserService;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<User>?> GetUsers()
    {
        List<User>? users = new List<User>();

        IEnumerable<UserDTO>? userDTOs = new List<UserDTO>();

        var authResult = await _client.GetAsync("api/Auth/getallusers");
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.StatusCode == HttpStatusCode.Unauthorized) {
            throw new RequestErrorExeption(authResult.StatusCode, "Please login to access this resource.");
        } else if (authResult.StatusCode == HttpStatusCode.Forbidden) {
            throw new RequestErrorExeption(authResult.StatusCode, "You do not have access to this resource.");
        } else if (authResult.IsSuccessStatusCode == false) {
            throw new RequestErrorExeption(authResult.StatusCode, authContent);
        }

        userDTOs = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (userDTOs == null)
            return null;

        foreach (UserDTO userDTO in userDTOs) {
            users.Add(new User().MapUser(userDTO));
        }

        return users;
    }

    public async Task<User> RegisterUser(UserDTO userDTO)
    {
        var authResult = await _client.PostAsJsonAsync("api/Auth/register", userDTO);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.StatusCode == HttpStatusCode.Unauthorized) {
            throw new RequestErrorExeption(authResult.StatusCode, "Please login to access this resource.");
        }
        else if (authResult.StatusCode == HttpStatusCode.Forbidden) {
            throw new RequestErrorExeption(authResult.StatusCode, "You do not have access to this resource.");
        }
        else if (authResult.IsSuccessStatusCode == false) {
            throw new RequestErrorExeption(authResult.StatusCode, authContent);
        }

        User user = new User();
        user.MapUser(userDTO);

        return user;
    }

    public async Task<User> UpdateUser(UserDTO userDTO)
    {
        var authResult = await _client.PutAsJsonAsync("api/Auth/update", userDTO);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.StatusCode == HttpStatusCode.Unauthorized) {
            throw new RequestErrorExeption(authResult.StatusCode, "Please login to access this resource.");
        }
        else if (authResult.StatusCode == HttpStatusCode.Forbidden) {
            throw new RequestErrorExeption(authResult.StatusCode, "You do not have access to this resource.");
        }
        else if (authResult.IsSuccessStatusCode == false) {
            throw new RequestErrorExeption(authResult.StatusCode, authContent);
        }

        var result = JsonSerializer.Deserialize<UserDTO>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        User user = new User();
        user.MapUser(userDTO);

        return user;
    }

    public async Task DeleteUser(UserDTO userDTO)
    {
        var authResult = await _client.PutAsJsonAsync("api/Auth/delete", userDTO);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.StatusCode == HttpStatusCode.Unauthorized) {
            throw new RequestErrorExeption(authResult.StatusCode, "Please login to access this resource.");
        }
        else if (authResult.StatusCode == HttpStatusCode.Forbidden) {
            throw new RequestErrorExeption(authResult.StatusCode, "You do not have access to this resource.");
        }
        else if (authResult.IsSuccessStatusCode == false) {
            throw new RequestErrorExeption(authResult.StatusCode, authContent);
        }
    }
}
