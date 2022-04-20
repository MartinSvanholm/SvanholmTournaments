using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared.Data.UserRolesData;

public interface IUserRolesData
{
    Task DeleteRolesForUser(User user);
    Task<IEnumerable<int>> GetRolesForUser(int userId);
    Task InsertRoleForUser(User user, int roleId);
}