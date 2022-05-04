using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Server.Services.DatHostAccountService;

public interface IDatHostAccountService
{
    Task<DatHostAccount> GetDatHostAccountById(int id);
    Task InsertDatHostAccountAsync(DatHostAccount datHostAccount);
}