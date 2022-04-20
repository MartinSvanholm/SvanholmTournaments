using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DbAccess;

namespace SvanholmTournaments.Shared.Data.UserRolesData;

public class UserRolesData : IUserRolesData
{
    private readonly ISqlDataAccess _db;

    public UserRolesData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<int>> GetRolesForUser(int userId)
    {
        var result = await _db.LoadData<int, dynamic>(storedProcedure: "dbo.spUserRoles_Get", new { UserId = userId });
        return result;
    }

    public async Task InsertRoleForUser(User user, int roleId) => await _db.SaveData(storedProcedure: "dbo.spUserRoles_Insert", new { UserId = user.Id, RoleId = roleId });

    public async Task DeleteRolesForUser(User user) => await _db.SaveData(storedProcedure: "dbo.spUserRoles_DeleteRolesForUser", new { UserId = user.Id });
}