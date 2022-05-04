using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SvanholmTournaments.Shared.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "UserDB")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "UserDB")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TRETURN>> MultiMapLoad<TFIRST, TSECOND, TRETURN>(string storedProcedure, Func<TFIRST, TSECOND, TRETURN> func, string connectionId = "UserDB")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        var test = connection.Query<TFIRST, TSECOND, TRETURN>(storedProcedure, map: func, commandType: CommandType.StoredProcedure);

        return test;
    }
}