using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared.Data.RoleData;

public interface IRoleData
{
    Task<Role?> GetRoleByName(string roleName);
    Task<IEnumerable<Role>> GetRoles();
    Task<IEnumerable<Role>> GetRolesById(int roleId);
    Task InsertRole(string roleName);
}