using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared.Data.DatHostAccountData;

public interface IDathostAccontData
{
    Task<DatHostAccount> GetDatHostByIdAsync(int id);
    Task InsertDatHostAccountAsync(DatHostAccount datHostAccount);
}