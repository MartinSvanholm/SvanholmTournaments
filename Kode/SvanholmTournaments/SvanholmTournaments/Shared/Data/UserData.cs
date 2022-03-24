using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DbAccess;

namespace SvanholmTournaments.Shared.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<User>> GetUsers() => _db.LoadData<User, dynamic>(storedProcedure: "dbo.spUser:GetAll", new { });

    public async Task<User?> GetUserById(int id)
    {
        var results = await _db.LoadData<User, dynamic>(storedProcedure: "dbo.spUser_Get", new { Id = id });

        return results.FirstOrDefault();
    }

    public Task InsertUser(User user) => _db.SaveData(storedProcedure: "dbo.spUser_Insert", new { user.FirstName, user.LastName });

    public Task UpdateUser(User user) => _db.SaveData(storedProcedure: "dbo.spUser_Update", user);

    public Task DeleteUser(int id) => _db.SaveData(storedProcedure: "dbo.spUser_Acchive", new { Id = id });
}