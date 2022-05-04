
namespace SvanholmTournaments.Shared.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "UserDB");
    Task<IEnumerable<TRETURN>> MultiMapLoad<TFIRST, TSECOND, TRETURN>(string storedProcedure, Func<TFIRST, TSECOND, TRETURN> func, string connectionId = "UserDB");
    Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "UserDB");
}