using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DbAccess;

namespace SvanholmTournaments.Shared.Data.DatHostAccountData;

public class DathostAccontData : IDathostAccontData
{
    private readonly ISqlDataAccess _db;

    public DathostAccontData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<DatHostAccount> GetDatHostByIdAsync(int id)
    {
        var result = await _db.LoadData<DatHostAccount, dynamic>(storedProcedure: "spDHAccount_GetById", new { Id = id });
        DatHostAccount? datHostAccount = result.FirstOrDefault();

        if (datHostAccount != null)
            return datHostAccount;
        else
            throw new ArgumentException("Could not find Account");
    }

    public async Task InsertDatHostAccountAsync(DatHostAccount datHostAccount)
    {
        await _db.SaveData(storedProcedure: "spDHAccount_Insert", new { datHostAccount.Email, datHostAccount.Password });
    }
}
