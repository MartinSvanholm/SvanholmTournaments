using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Client.Services.RoleService;

public interface IRoleService
{
    Task<IEnumerable<Role>?> GetRolesAsync();
    Task CreateRoleAsync(Role role);
}