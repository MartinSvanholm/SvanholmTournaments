using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DbAccess;

namespace SvanholmTournaments.Shared.Data.RoleData;

public class RoleData : IRoleData
{
    private readonly ISqlDataAccess _db;

    public RoleData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Role>> GetRoles() => await _db.LoadData<Role, dynamic>(storedProcedure: "dbo.spRole_GetAll", new { });

    public async Task<Role?> GetRoleByName(string roleName)
    {
        var result = await _db.LoadData<Role, dynamic>(storedProcedure: "dbo.spRole_Get", new { RoleName = roleName });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<Role>> GetRolesById(int roleId) => await _db.LoadData<Role, dynamic>(storedProcedure: "dbo.spRole_GetById", new { Id = roleId });

    public async Task InsertRole(string roleName)
    {
        try {
            await _db.SaveData(storedProcedure: "dbo.spRole_Insert", new { RoleName = roleName });
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }
    }
}
