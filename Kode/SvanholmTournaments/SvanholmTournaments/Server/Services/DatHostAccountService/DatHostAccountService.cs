using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Data.DatHostAccountData;

namespace SvanholmTournaments.Server.Services.DatHostAccountService;

public class DatHostAccountService : IDatHostAccountService
{
    private readonly IDathostAccontData _dathostAccontData;

    public DatHostAccountService(IDathostAccontData dathostAccontData)
    {
        _dathostAccontData = dathostAccontData;
    }

    public async Task<DatHostAccount> GetDatHostAccountById(int id)
    {
        try {
            DatHostAccount dathostAccount = await _dathostAccontData.GetDatHostByIdAsync(id);
            return dathostAccount;
        }
        catch (Exception) {
            throw;
        }
    }

    public async Task InsertDatHostAccountAsync(DatHostAccount datHostAccount)
    {
        try {
            await _dathostAccontData.InsertDatHostAccountAsync(datHostAccount);
        }
        catch (Exception) {
            throw;
        }
    }
}