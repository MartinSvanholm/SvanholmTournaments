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

    public async Task<IEnumerable<User>> GetUsers() => await _db.LoadData<User, dynamic>(storedProcedure: "dbo.spUser_GetAll", new { });

    public async Task<User?> GetUserByUsername(string username)
    {
        var results = await _db.LoadData<User, dynamic>(storedProcedure: "dbo.spUser_Get", new { Username = username });

        return results.FirstOrDefault();
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