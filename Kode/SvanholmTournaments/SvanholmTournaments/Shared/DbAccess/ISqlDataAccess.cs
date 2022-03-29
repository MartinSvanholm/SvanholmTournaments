
namespace SvanholmTournaments.Shared.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "UserDB");
    Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "UserDB");
}