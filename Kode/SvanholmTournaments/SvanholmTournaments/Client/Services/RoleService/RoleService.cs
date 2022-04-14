using System.Net;
using System.Text.Json;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Exeptions;

namespace SvanholmTournaments.Client.Services.RoleService;

public class RoleService : IRoleService
{
    private readonly HttpClient _client;

    public RoleService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Role>?> GetRolesAsync()
    {
        IEnumerable<Role>? roles = new List<Role>();

        var authResult = await _client.GetAsync("api/Role/getall");
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

        roles = JsonSerializer.Deserialize<IEnumerable<Role>>(authContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return roles;
    }

    public async Task CreateRoleAsync(Role role)
    {
        var authResult = await _client.PostAsJsonAsync("api/Role/create", role);
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
