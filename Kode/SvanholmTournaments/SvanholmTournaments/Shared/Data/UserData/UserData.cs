using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Data.RoleData;
using SvanholmTournaments.Shared.Data.UserRolesData;
using SvanholmTournaments.Shared.DbAccess;

namespace SvanholmTournaments.Shared.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;
    private readonly IUserRolesData _userRolesData;
    private readonly IRoleData _roleData;

    public UserData(ISqlDataAccess db, IUserRolesData userRolesData, IRoleData roleData)
    {
        _db = db;
        _userRolesData = userRolesData;
        _roleData = roleData;
    }

    public async Task<IEnumerable<User>> GetUsers() => await _db.LoadData<User, dynamic>(storedProcedure: "dbo.spUser_GetAll", new { });

    public async Task<User?> GetUserByUsername(string username)
    {
        var results = await _db.LoadData<User, dynamic>(storedProcedure: "dbo.spUser_Get", new { Username = username });

        User? user = results.FirstOrDefault();

        if (user == null)
            return null;

        var roleIds = await _userRolesData.GetRolesForUser(user.Id);

        foreach (var roleId in roleIds) {
            user.Roles = (List<Role>) await _roleData.GetRolesById(roleId);
        }

        return user;
    }

    public async Task InsertUser(User user) => await _db.SaveData(
        storedProcedure: "dbo.spUser_Insert",
        new {
            user.FirstName,
            user.LastName,
            user.Username,
            user.CreatedDate,
            user.PasswordHash,
            user.PasswordSalt
        });

    public async Task UpdateUser(User user) => await _db.SaveData(storedProcedure: "dbo.spUser_Update", user);

    public async Task DeleteUser(int id) => await _db.SaveData(storedProcedure: "dbo.spUser_Acchive", new { Id = id });
}